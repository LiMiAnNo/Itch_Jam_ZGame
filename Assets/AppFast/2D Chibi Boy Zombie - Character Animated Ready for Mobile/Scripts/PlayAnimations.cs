using System;
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace NetDinamica.AppFast
{
    public class PlayAnimations : MonoBehaviour
    {
        public Transform target; // Player
        public float moveSpeed = 2f;

        [Header("Wandering Settings")]
        public float wanderRadius = 5f;
        public float waitTime = 2f;

        private Vector2 wanderOrigin;
        private Vector2 wanderTarget;
        private bool isWaiting = false;

        private Animator anim;
        private bool facingRight = true;
        private bool isManualAnim = false;
        
        public List<GameObject> items = new List<GameObject>();

        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            wanderOrigin = transform.position;
            PickNewWanderPoint();
        }

        void Update()
        {
            // Manual animation testing
            isManualAnim = false;

            if (Input.GetKeyDown(KeyCode.Alpha1)) { anim.Play("Idle"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { anim.Play("Walk"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { anim.Play("Attack"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) { anim.Play("Hurt"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha9)) { anim.Play("Die"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.F)) { Flip(); }

            // AI movement
            if (!isManualAnim)
            {
                if (target != null)
                {
                    MoveTowardsTarget(target.position);
                }
                else if (!isWaiting)
                {
                    Wander();
                }
            }
        }

        void MoveTowardsTarget(Vector2 targetPos)
        {
            Vector2 direction = targetPos - (Vector2)transform.position;

            // Flip sprite based on X direction
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }

            if (direction.magnitude > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                anim.Play("Walk");
            }
            else
            {
                anim.Play("Idle");
            }
        }

        void Wander()
        {
            float distance = Vector2.Distance(transform.position, wanderTarget);

            if (distance > 0.1f)
            {
                MoveTowardsTarget(wanderTarget);
            }
            else
            {
                anim.Play("Idle");
                StartCoroutine(WaitAndPickNewWanderPoint());
            }
        }

        IEnumerator WaitAndPickNewWanderPoint()
        {
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            PickNewWanderPoint();
            isWaiting = false;
        }

        void PickNewWanderPoint()
        {
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            wanderTarget = wanderOrigin + randomOffset;
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        //void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        ////PlayerInventory player = other.GetComponent<PlayerInventory>();
        //        //if (player == null || player.carriedItem == null)
        //        //    return;

        //        //if (items.Contains(player.carriedItem))
        //        //{
        //        //    items.Remove(player.carriedItem);
        //        //    Debug.Log("Item given: " + player.carriedItem.name);

        //        //    // Optional: play animation, sound, or feedback
        //        //    player.carriedItem = null; // Remove item from player
        //        //}
        //        else
        //        {
        //            Debug.Log("This item is not needed.");
        //        }
        //    }
        //}
    }
}
