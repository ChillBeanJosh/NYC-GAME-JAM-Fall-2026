using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    static TransitionManager instance;
    public static TransitionManager Instance => instance;

    Camera cam;

    [SerializeField] CameraPositionList cameraPositions;

    CameraPosition currentCameraPosition;

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
        //Give player invincibility / 
    }
}

[System.Serializable]
public enum CameraPosition
{
    StreetTopLeft,
    StreetTopRight,
    StreetBottomLeft,
    StreetBottomRight,
    SubwayTop,
    SubwayBottom
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