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
        anim.SetBool("NotMoving", movement.magnitude < .1f);

        anim.SetBool("MovingX", movement.x != 0);
        anim.SetFloat("MovingY", movement.y);

        if (movement.x != 0)
        {
            flipX = 0 > movement.x;
        }
        sr.flipX = flipX;
    }
}
