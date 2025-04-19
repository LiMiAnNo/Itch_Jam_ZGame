using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    Rigidbody2D rb;
    Vector2 playerMovement;
    Animator animator;

    int health; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = 100;

    }

    void takeDamage(int dmg)
    {
        health -= dmg;


        animator.SetTrigger("TookDamage");
        if(health <= 0)
        {
            animator.SetBool("Dead",true);
        }
    }

    // Update is called once per frame
    void Update()
    {

       

    }

    private void FixedUpdate()
    {
       
        CharacterMovement();
    }

    void flipSprite()
    {
        if (rb.linearVelocity.normalized.x < 0 )
        {
            this.transform.rotation = new Quaternion(1, 180, 0, 0);
        }
        else if(rb.linearVelocity.normalized.x > 0)
        {
            this.transform.rotation = Quaternion.identity;
        }

    }

    void CharacterMovement()
    {
        if (health <= 0) { return; }
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        Vector2 playerVelocity = playerMovement * 200.0f * Time.fixedDeltaTime;

        rb.linearVelocity = playerVelocity;

        animator.SetInteger("Velocity", (int)playerVelocity.magnitude);

        flipSprite(); 
    }
}
