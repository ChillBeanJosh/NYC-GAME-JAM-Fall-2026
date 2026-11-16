using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    static TransitionManager instance;
    public static TransitionManager Instance => instance;

    public PositionLogger logger;

    Camera cam;

    [SerializeField] CameraPositionList cameraPositions;

    CameraPosition currentCameraPosition; 
    public CameraPosition GetCurrentCameraPosition => currentCameraPosition;

    private void Awake()
    {
        instance = this;

        cam = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPosition(PlayerMovement playerCharacter, CameraPosition newCameraPosition, Vector3 addOffset)
    {
        if (newCameraPosition == currentCameraPosition)
        {
            return;
        }
        currentCameraPosition = newCameraPosition;

        if (cameraPositions.Has(newCameraPosition))
        {
            cam.transform.position = cameraPositions.Get(newCameraPosition);
        }
        playerCharacter.SetCurrentPosition(playerCharacter.transform.position + addOffset);
        logger.testRound = (int)newCameraPosition;

        //Give player invincibility / 
    }
}

[System.Serializable]
public enum CameraPosition
{
    StartStreet = 0,
    StartTurnstile = 1,
    StartSubway = 2,
    SubwayCar = 3,
    EndSubway = 4,
    EndTurnstile = 5,
    EndStreet = 6
}

[System.Serializable]
public class CameraPositionList
{
    public SavedCameraPosition[] savedPositions;

    public bool Has(CameraPosition cameraPosition)
    {
        for (int i = 0; i < savedPositions.Length; i++)
        {
            if (savedPositions[i].camPositionType == cameraPosition)
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 Get(CameraPosition cameraPosition)
    {
        for (int i = 0; i < savedPositions.Length; i++)
        {
            if (savedPositions[i].camPositionType == cameraPosition)
            {
                return savedPositions[i].savedPosition;
            }
        }
        return Vector3.zero;
    }
}

[System.Serializable]
public class SavedCameraPosition
{
    public CameraPosition camPositionType;
    public Vector3 savedPosition;
}