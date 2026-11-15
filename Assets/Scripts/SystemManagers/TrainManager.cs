using UnityEngine;
using System;
using System.Collections;

public class TrainManager : MonoBehaviour
{
    [SerializeField] float trainArriveTime;
    [SerializeField] float trainStopTime;

    [SerializeField] float doorOpenTime;
    [SerializeField] CameraPosition currentExitCameraPosition;

    [SerializeField] GameObject[] doorObjects;

    const CameraPosition entrancePosition = CameraPosition.StartSubway;
    const CameraPosition exitPosition = CameraPosition.EndSubway;

    float doorOpenCloseTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnRoundStart()
    {
        StartCoroutine("StupidCoroutineForTheTrainAHHHHHHHHHHHHHHHHHHHHHHHH");
    }

    public IEnumerator StupidCoroutineForTheTrainAHHHHHHHHHHHHHHHHHHHHHHHH()
    {
        yield return new WaitForSeconds(1f);
    }

    void OpenDoor()
    {
        for (int i = 0; i < doorObjects.Length; i++)
        {
            doorObjects[i].SetActive(false);
        }
    }

    void CloseDoor()
    {
        for (int i = 0; i < doorObjects.Length; i++)
        {
            doorObjects[i].SetActive(true);
        }
    }
}

public enum TrainStatus
{
    NotStation
}
