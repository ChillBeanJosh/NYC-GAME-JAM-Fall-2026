using UnityEngine;
using FMODUnity;
using UnityEngine.Rendering;
using System;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TransitionManager manager;
    public GameObject Player;
    [SerializeField] EventReference Footsteps;
    [SerializeField] private PlayerMovement playerMovement;
    public float time = 1f;
    public float rate = 1f;
    private int location = 0;

    public CameraPosition cameraPosition;
    
    

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = TransitionManager.Instance.GetCurrentCameraPosition;

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
