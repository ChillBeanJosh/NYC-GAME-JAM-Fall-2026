using UnityEngine;
using System.Collections.Generic;

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
    public List<levelDataLog> levelSegments = new List<levelDataLog>();
    
    
    [SerializeField] private float timeStepDuration = 0.1f;
    private float timeStepTimer = 0f;
    private float dt = 0f;
    
    
    private void Update()
    { 
        //For Each Level: 
        for (int i =  0; i < levelSegments.Count; i++)
        {
            Debug.Log("Current Iteration: " + i);
            //Repeat Logger While We Are On The Current Level:
            if (levelManager.Round == levelSegments[i].level)
            {
                
                Debug.Log("Printing out Current Log of " + levelSegments[i].level);
                //Timer Logic:
                dt += Time.deltaTime; 
                timeStepTimer += Time.deltaTime;
                
                //Log Data of Current Level When When timeStep Is Reached:
                if (timeStepTimer >= timeStepDuration)
                {
                    Debug.Log("Time step has been achieved... Stamping Data!!!");
                    LogData(dt, levelSegments[i].level);
                    timeStepTimer = 0f;
                }
            }
        }
    }
    
    private void LogData(float deltaTime, int i)
    {
        Debug.Log("Data Logged!");
        levelSegments[i]._timeValues.Add(deltaTime);
        levelSegments[i]._positionValues.Add(player.GetCurrentPosition());
    }
    
}
