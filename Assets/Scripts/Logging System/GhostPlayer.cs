using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GhostData
{
    public int level;

    public List<float> timeValues = new List<float>();
    public List<Vector2> positionValues = new List<Vector2>();
}

public class GhostPlayer : MonoBehaviour
{
    public List<GhostData> ghostSegments = new List<GhostData>();

    public void StoreLevelData(int level, List<float> times, List<Vector2> positions)
    {
        GhostData newGhost = new GhostData();

        //Create independent copies of the data:
        newGhost.level = level;
        newGhost.timeValues = new List<float>(times);
        newGhost.positionValues = new List<Vector2>(positions);

        //Store the completed level:
        ghostSegments.Add(newGhost);
        Debug.Log("Ghost data stored for level: " + level);
    }
}
