using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class EnemyMovement : MonoBehaviour
    {
        public float enemyChasingSpeed = 2;

        public Transform targetToFollow;
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
    }
}
