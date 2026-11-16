using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct levelDataLog
{
    public int level;
    public  List<float> _timeValues;
    public  List<Vector2> _positionValues;
}

public class PositionLogger : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameManager levelManager;
    [SerializeField] private GhostPlayer ghostPlayer;

    public List<levelDataLog> levelSegments = new List<levelDataLog>();
     
    [SerializeField] private float timeStepDuration = 0.1f;
    private float timeStepTimer = 0f;
    private float dt = 0f;
    private int previousRound = -1;

    public int testRound = 0;
    private bool isTestingRoundTransition = false;


    private void Start()
    {
        GameManager.Instance.RoundStartEvent.AddListener(NewRound);
    }

    private void Update()
    {
        //int currentRound = levelManager.Round % levelSegments.Count;
        int currentRound = testRound;

        //Update previous as current, to ensure dt clear on next level:
        if (currentRound != previousRound)
        {
            //dt = 0f;
            timeStepTimer = 0f;
            previousRound = currentRound;
        }

        //For Each Level: 
        for (int i =  0; i < levelSegments.Count; i++)
        {
            //Repeat Logger While We Are On The Current Level:
            if (currentRound == levelSegments[i].level)
            {
                //Timer Logic:
                dt += Time.deltaTime;
                timeStepTimer += Time.deltaTime;

                //Log Data of Current Level When When timeStep Is Reached:
                if (timeStepTimer >= timeStepDuration)
                {
                    LogData(dt, i);
                    timeStepTimer = 0f;
                }
            }
            //We are on a Round that does not match the Logger Data:
            else
            {
                //Logger isnt Empty, and has at least 1 entry:
                if (levelSegments[i]._timeValues != null && levelSegments[i]._positionValues != null && levelSegments[i]._timeValues.Count > 0 && levelSegments[i]._positionValues.Count > 0)
                {
                    TransferData(i);
                }
            }
        }
    }

    public void NewRound()
    {
        dt = 0;
    }

    private void LogData(float deltaTime, int i)
    {
        levelSegments[i]._timeValues.Add(deltaTime);
        levelSegments[i]._positionValues.Add(player.GetCurrentPosition());
    }

    private void TransferData(int i)
    {
        //Send Data into a GhostPlayer, and store it:
        ghostPlayer.StoreLevelData(levelSegments[i].level, levelSegments[i]._timeValues, levelSegments[i]._positionValues);

        //Clear the levels logger data to log for next round instance:
        levelSegments[i]._timeValues.Clear();
        levelSegments[i]._positionValues.Clear();
    }

    public void CompleteTestRound()
    {
        if (!isTestingRoundTransition)
        {
            StartCoroutine(TestRoundTransition());
        }
    }

    private IEnumerator TestRoundTransition()
    {
        isTestingRoundTransition = true;

        //Increment testRound to force data transfer:
        testRound++;

        //Wait until the current frame finishes:
        yield return new WaitForEndOfFrame();

        //Return to Level 0 to trigger ghost playback:
        testRound--;

        dt = 0f;
        isTestingRoundTransition = false;
    }
}
