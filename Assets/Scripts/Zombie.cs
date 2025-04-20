using UnityEngine;
using System.Collections;
public class Zombie : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRadius = 5f;
    public float hesitationDistance = 1.5f;
    public float roamRadius = 3f;
    public float freezeDuration = 2f;

    private Transform player;
    private Vector2 roamTarget;
    private Rigidbody2D rb;
    private bool isFrozen = false;
    private bool hasTeddyEar = false; // This can be set externally based on item
    private enum State { Roaming, Chasing, Frozen }
    private State currentState = State.Roaming;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNewRoamTarget();
        
        
        
    }

    void Update()
    {
        if (isFrozen) return;

        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Roaming:
                Roam();
                if (distance < detectionRadius) currentState = State.Chasing;
                break;

            case State.Chasing:
                if (distance > detectionRadius)
                {
                    currentState = State.Roaming;
                    SetNewRoamTarget();
                }
                else
                {
                    ChasePlayer(distance);
                }
                break;
        }
    }

    void Roam()
    {
        Vector2 direction = (roamTarget - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, roamTarget) < 0.2f)
            SetNewRoamTarget();
    }

    void SetNewRoamTarget()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(1f, roamRadius);
        roamTarget = (Vector2)transform.position + randomDirection;
    }

    void ChasePlayer(float distance)
    {
        if (distance < hesitationDistance && hasTeddyEar)
        {
            // Hesitate
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        }
    }

    public void FreezeZombie()
    {
        if (!isFrozen)
            StartCoroutine(FreezeRoutine());
    }

    IEnumerator FreezeRoutine()
    {
        isFrozen = true;
        currentState = State.Frozen;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(freezeDuration);
        isFrozen = false;
        currentState = State.Roaming;
        SetNewRoamTarget();
    }

    public void SetTeddyEar(bool value)
    {
        hasTeddyEar = value;
    }

    public void AttractTo(Vector2 position)
    {
        // Moves zombie toward a distraction (like Echo Mushroom)
        StopAllCoroutines();
        StartCoroutine(MoveToPoint(position));
    }

    IEnumerator MoveToPoint(Vector2 point)
    {
        float t = 0;
        while (Vector2.Distance(transform.position, point) > 0.2f && t < 5f)
        {
            Vector2 dir = (point - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        currentState = State.Roaming;
        SetNewRoamTarget();
    }
}
