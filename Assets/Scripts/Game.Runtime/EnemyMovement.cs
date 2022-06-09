using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class EnemyMovement : MonoBehaviour
    {
        public float enemyChasingSpeed = 2;

        public int enemyDamage;
        public Vector2 gizmosAttackSize;

        public Transform attackPoint;
        public Transform targetToFollow;
        public LayerMask playerLayer;

        private AIOverlapDetector detector;
        private Animator anim;

        private bool isWalking;

        private void Awake()
        {
            detector = GetComponent<AIOverlapDetector>();
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            isWalking = true;
        }

        // Update is called once per frame
        void Update()
        {
            SearchForPlayer();
            UpdateAnimation();
        }

        void UpdateAnimation()
        {
            anim.SetBool("isWalking", isWalking);
        }

        private void CheckDirection()
        {
            Vector3 tmpScale = transform.localScale;

            if (transform.position.x < targetToFollow.position.x)
            {
                tmpScale.x = -1;
            } else
            {
                tmpScale.x = 1;
            }
            transform.localScale = tmpScale;    
        }

        private void SearchForPlayer()
        {
            if (detector.playerInArea)
            {
                GetComponent<WaypointFollower>().enabled = false;
                CheckDirection();
                transform.position = Vector2.MoveTowards(transform.position, targetToFollow.position, enemyChasingSpeed * Time.deltaTime);
                if (detector.playerInHitArea)
                {
                    isWalking = false;
                    enemyChasingSpeed = 0;
                    anim.SetTrigger("Attack_2");
                } else
                {
                    isWalking = true;
                    enemyChasingSpeed = 2;
                }
            } else
            {
                GetComponent<WaypointFollower>().enabled = true;
            }
        }

        private void Attack() // set in event animation
        {
            Collider2D playerHit = Physics2D.OverlapBox(attackPoint.position, gizmosAttackSize, playerLayer);

            if (playerHit != null)
            {
                playerHit.GetComponent<PlayerHealthSystem>().TakeDamage(this.enemyDamage);
            } else
            {
                return;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(attackPoint.position, gizmosAttackSize);
        }
    }
}
