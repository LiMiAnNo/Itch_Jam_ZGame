using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.UI; // <-- Correct for UI components like Image

namespace NetDinamica.AppFast
{
    public class PlayAnimations : MonoBehaviour
    {
        public Canvas itemCanvas;

        public Transform target; // Player
        public float moveSpeed = 2f;

        [Header("Wandering Settings")]
        public float wanderRadius = 5f;
        public float waitTime = 2f;

        private Vector2 wanderOrigin;
        private Vector2 wanderTarget;
        private bool isWaiting = false;

        private Image[] itemImages;
        private Animator anim;
        private bool facingRight = true;
        private bool isManualAnim = false;

        public List<Sprite> itemSprites = new List<Sprite>(); // <-- Use Sprites for UI images

        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            itemCanvas = GetComponentInChildren<Canvas>();

            if (itemCanvas != null)
            {
                itemImages = itemCanvas.GetComponentsInChildren<Image>();
            }
            else
            {
                Debug.LogWarning("Item canvas not found!");
            }

            wanderOrigin = transform.position;
            PickNewWanderPoint();

            ShowNeededItems();
        }

        void Update()
        {
            isManualAnim = false;

            if (Input.GetKeyDown(KeyCode.Alpha1)) { anim.Play("Idle"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { anim.Play("Walk"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha5)) { anim.Play("Attack"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha7)) { anim.Play("Hurt"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.Alpha9)) { anim.Play("Die"); isManualAnim = true; }
            else if (Input.GetKeyDown(KeyCode.F)) { Flip(); }

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

        void OnTriggerEnter2D(Collider2D other)
        {
            /*if (other.CompareTag("Player"))
            {
                PlayerInventory player = other.GetComponent<PlayerInventory>();
                if (player == null || player.carriedItem == null) return;

                if (itemSprites.Contains(player.carriedItem))
                {
                    Debug.Log("Item accepted: " + player.carriedItem.name);
                    itemSprites.Remove(player.carriedItem);
                    ShowNeededItems(); // Update UI
                    player.carriedItem = null;
                }
                else
                {
                    Debug.Log("Item not needed.");
                }
            }*/
        }

        void ShowNeededItems()
        {
            if (itemImages == null || itemSprites == null) return;

            for (int i = 0; i < itemImages.Length; i++)
            {
                if (i < itemSprites.Count)
                {
                    itemImages[i].sprite = itemSprites[i];
                    itemImages[i].enabled = true;
                }
                else
                {
                    itemImages[i].enabled = false;
                }
            }
        }
    }
}
