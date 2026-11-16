using UnityEngine;
using FMODUnity;
using UnityEngine.Rendering;
using System;
using static UnityEditor.Profiling.RawFrameDataView;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TransitionManager manager;
    public GameObject Player;
    [SerializeField] EventReference Footsteps;
    //[SerializeField] EventReference Ambience;
    private FMOD.Studio.EventInstance Ambience;
    public FMODUnity.EventReference Ambiencee;
    [SerializeField] EventReference Music;
    [SerializeField] private PlayerMovement playerMovement;
    public float time = 1f;
    public float rate = 1f;
    private int location = 0;

    public CameraPosition cameraPosition;
    
    

    
    void Start()
    {
        RuntimeManager.PlayOneShot(Music);
        Ambience = FMODUnity.RuntimeManager.CreateInstance(Ambiencee);
        Ambience.start();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = TransitionManager.Instance.GetCurrentCameraPosition;
        var bleh = cameraPosition.ToString();

        Ambience.setParameterByName("location", location);


        if (bleh == "StartStreet") {
            location = 0;
        } else if (bleh == "StartTurnstile"){
            location = 1;
        } else if (bleh == "StartSubway"){
            location = 1;
        } else if (bleh == "SubwayCar") {
            location = 2;
        } else if (bleh == "EndSubway"){
            location = 3;
        } else if(bleh == "EndTurnstile"){
            location = 3;
        } else if(bleh == "EndStreet")
        {
            location = 4;
        }


        Vector3 playerVelocity = playerMovement.GetCurrentVelocity();
        float playerSpeed = playerVelocity.magnitude;
        time += Time.deltaTime;
        if (playerSpeed != 0)
        {
            if (time >= rate)
            {
                RuntimeManager.PlayOneShot(Footsteps);
                time = 0f;
            }
        }
    }


}
