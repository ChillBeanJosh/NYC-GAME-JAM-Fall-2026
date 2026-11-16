using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UpdateAnimation(Vector2 movement)
    {
        //if ()
    }
}
