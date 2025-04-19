using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    Rigidbody2D rb;
    Vector2 playerMovement;
    Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       


    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + playerMovement * 5.0f * Time.fixedDeltaTime);
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        Vector2 playerVelocity = playerMovement * 200.0f * Time.fixedDeltaTime;

        rb.linearVelocity = playerVelocity;

        animator.SetInteger("xVelocity", (int)playerVelocity.x);
        animator.SetInteger("yVelocity", (int)playerVelocity.y);


    }
}
