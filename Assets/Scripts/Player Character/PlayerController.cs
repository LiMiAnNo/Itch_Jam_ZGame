using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private PlayerInventory inventory;

    Rigidbody2D rb;
    Vector2 playerMovement;
    Animator animator;
    
    //int health; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //health = 100;
        inventory = GetComponent<PlayerInventory>();


    }

    //void takeDamage(int dmg)
    //{
    //    health -= dmg;


    //    animator.SetTrigger("TookDamage");
    //    if(health <= 0)
    //    {
    //        animator.SetBool("Dead",true);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (inventory != null && inventory.HasItem("Memory Shard"))
            {
                FreezeClosestZombie();
                // Optional: Remove from inventory after one use
                // inventory.RemoveItem("Memory Shard");
            }
        }
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
        //if (health <= 0) { return; }
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        Vector2 playerVelocity = playerMovement * 200.0f * Time.fixedDeltaTime;

        rb.linearVelocity = playerVelocity;

        animator.SetInteger("Velocity", (int)playerVelocity.magnitude);

        flipSprite(); 
    }

    void FreezeClosestZombie()
    {
        Zombie[] allZombies = Object.FindObjectsByType<Zombie>(FindObjectsSortMode.None);

        if (allZombies.Length == 0) return;

        Zombie closest = null;
        float minDist = Mathf.Infinity;

        foreach (Zombie z in allZombies)
        {
            float dist = Vector2.Distance(transform.position, z.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = z;
            }
        }

        if (closest != null)
        {
            closest.FreezeZombie();
            Debug.Log($"Froze {closest.name} with Memory Shard.");
        }
    }

}
