using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class WaypointFollower : MonoBehaviour
    {
        [SerializeField] private GameObject[] wayPoints;
        [SerializeField] private float movingSpeed = 2f;

        private Animator anim;

        private int currentWaypointIndex = 0;
        private bool isWalking;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        void Update()
        {
            FollowWaypoint();
        }

        private void FollowWaypoint()
        {
            if (Vector2.Distance(transform.position, wayPoints[currentWaypointIndex].transform.position) < .2f)
            {
                currentWaypointIndex++;
                transform.Rotate(new Vector3(0f, 180f, 0f));

                if (currentWaypointIndex >= wayPoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, Time.deltaTime * movingSpeed);
        }
    }
}
