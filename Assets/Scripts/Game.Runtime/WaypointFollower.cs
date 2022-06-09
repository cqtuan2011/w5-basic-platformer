using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class WaypointFollower : MonoBehaviour
    {
        [SerializeField] private GameObject[] wayPoints;
        [SerializeField] private float movingSpeed = 2f;


        private int currentWaypointIndex = 0;

        void Update()
        {
            FollowWaypoint();
        }

        private void FollowWaypoint()
        {
            if (Vector2.Distance(transform.position, wayPoints[currentWaypointIndex].transform.position) < .2f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= wayPoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            Flip();
            
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, Time.deltaTime * movingSpeed);
        }

        private void Flip()
        {
            Vector3 tmpScale = gameObject.transform.localScale;

            if (wayPoints[currentWaypointIndex].transform.position.x < transform.position.x)
            {
                tmpScale.x = 1;
            } else
            {
                tmpScale.x = -1;
            }
            gameObject.transform.localScale = tmpScale;
        }
    }
}
