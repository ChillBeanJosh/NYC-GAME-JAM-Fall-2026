using UnityEngine;

public class TransitionCollider : MonoBehaviour
{
    [SerializeField] CameraPosition nextCameraPosition;
    [SerializeField] Vector3 nextPlayerOffset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement movement = collision.GetComponent<PlayerMovement>();
        if (movement)
        {
            TransitionManager.Instance.SwitchPosition(movement, nextCameraPosition, nextPlayerOffset);
        }
    }
}
