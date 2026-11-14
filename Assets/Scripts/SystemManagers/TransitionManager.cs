using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    static TransitionManager instance;
    public static TransitionManager Instance => instance;

    Camera cam;

    [SerializeField] Dictionary<CameraPosition, Vector3> cameraPositions;

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

        if (cameraPositions.ContainsKey(newCameraPosition))
        {
            cam.transform.position = cameraPositions[newCameraPosition];
        }
        playerCharacter.transform.position += addOffset;
        //Give player invincibility / 
    }
}

public enum CameraPosition
{
    StreetTopLeft,
    StreetTopRight,
    StreetBottomLeft,
    StreetBottomRight,
    SubwayTop,
    SubwayBottom
}
