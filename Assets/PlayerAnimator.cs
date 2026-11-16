using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;

    bool flipX;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UpdateAnimation(Vector2 movement)
    {
        anim.SetBool()
    }
}
