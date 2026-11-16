using System.Collections.Generic;
using UnityEngine;

public class GhostPlayback : MonoBehaviour
{
    [SerializeField] private GameManager levelManager;
    [SerializeField] private GhostPlayer ghostPlayer;
    [SerializeField] private GameObject ghostPrefab;
    private int previousRound = -1;

    private class ActiveGhost
    {
        public GhostData data;
        public Transform ghostTransform;

        public int sampleIndex;
        public int sampleSize;

        public float elapsedPlaybackTime;
    }
    private List<ActiveGhost> activeGhosts = new List<ActiveGhost>();


    [SerializeField] private bool destroyGhostAtEnd = false;
    public PositionLogger logger;

    private void LateUpdate()
    {
        //int currentRound = levelManager.Round % logger.levelSegments.Count;
        int currentRound = logger.testRound;

        //Update previous as current, clear old and spawn new ghosts as a result:
        if (currentRound != previousRound)
        {
            previousRound = currentRound;

            RemoveAllGhosts();
            SpawnGhostsForRound(currentRound);
        }
    }

    private void FixedUpdate()
    {
        for (int i = activeGhosts.Count - 1; i >= 0; i--)
        {
            ActiveGhost ghost = activeGhosts[i];
            UpdateGhost(ghost);

            //Destory early at final Position:
            if (destroyGhostAtEnd && ghost.elapsedPlaybackTime >= ghost.data.timeValues[ghost.sampleSize - 1])
            {
                Destroy(ghost.ghostTransform.gameObject);
                activeGhosts.RemoveAt(i);
            }
        }
        Physics2D.SyncTransforms();
    }

    private void SpawnGhostsForRound(int round)
    {
        foreach (GhostData data in ghostPlayer.ghostSegments)
        {
            //Only Spawn Ghosts matching the CurrentRound and have logged data:
            if (data == null || data.level != round) continue;
            if (data.timeValues == null || data.positionValues == null) continue;

            //Declare data size:
            int size = Mathf.Min(data.timeValues.Count, data.positionValues.Count);
            if (size == 0) continue;

            //1.) Spawn Ghost OBJ:
            GameObject newGhost = Instantiate(ghostPrefab, data.positionValues[0], Quaternion.identity);

            //2.) Assign Ghost OBJ Data:
            ActiveGhost activeGhost = new ActiveGhost();
            activeGhost.data = data;
            activeGhost.ghostTransform = newGhost.transform;
            activeGhost.sampleIndex = 0;
            activeGhost.sampleSize = size;
            activeGhost.elapsedPlaybackTime = 0f;

            //3.) Store Ghost OBJ in List:
            activeGhosts.Add(activeGhost);
        }
        Debug.Log("Current ghost count is: " + activeGhosts.Count);
    }

    private void UpdateGhost(ActiveGhost ghost)
    {
        ghost.elapsedPlaybackTime += Time.fixedDeltaTime;

        Vector2 targetPosition = GetInterpolatedPosition(ghost);
        ghost.ghostTransform.position = targetPosition;
    }

    private Vector2 GetInterpolatedPosition(ActiveGhost ghost)
    {
        List<float> times = ghost.data.timeValues;
        List<Vector2> positions = ghost.data.positionValues;

        int index = ghost.sampleIndex;
        int size = ghost.sampleSize;

        //Hold at the first position before its timestamp:
        if (ghost.elapsedPlaybackTime <= times[0]) return positions[0];

        //Hold at the final position after its timestamp:
        if (ghost.elapsedPlaybackTime >= times[size - 1]) return positions[size - 1];

        //Find the two positions surrounding the current playback time:
        while (index < size - 2 && ghost.elapsedPlaybackTime >= times[index + 1])
        {
            index++;
        }
        ghost.sampleIndex = index;

        //(t_0) & (t_1):
        float startTime = times[index];
        float endTime = times[index + 1];

        //(P_0) & (P_1):
        Vector2 startPosition = positions[index];
        Vector2 endPosition = positions[index + 1];

        //(t_1 - t_0):
        float segmentDuration = endTime - startTime;
        if (segmentDuration <= Mathf.Epsilon) return endPosition;

        //First Order Lagrange Interpolation Polynomial: 
        //(t - t_0) / (t_1 - t_0):
        float t = (ghost.elapsedPlaybackTime - startTime) / segmentDuration;
        return Vector2.Lerp(startPosition, endPosition, t);
    }

    private void RemoveAllGhosts()
    {
        foreach (ActiveGhost ghost in activeGhosts)
        {
            if (ghost.ghostTransform != null)
            {
                Destroy(ghost.ghostTransform.gameObject);
            }
        }
        activeGhosts.Clear();
    }
}
