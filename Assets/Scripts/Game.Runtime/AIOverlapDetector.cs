using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class AIOverlapDetector : MonoBehaviour
    {
        [SerializeField] Vector3 detectorSize;
        [SerializeField] Vector3 positionOffset;

        [SerializeField] Vector3 hitDetectorSize;
        [SerializeField] Vector3 hitPositionOffset;

        public float enemyChasingSpeed;
        public Transform hitBox;

        public LayerMask playerLayer;
        public Color gizmosColor;
        public Color detectedColor;
        public Color hitDetectedColor;

        public bool drawGizmos;

        [HideInInspector] public bool playerInArea;
        [HideInInspector] public bool playerInHitArea;

        private void Update()
        {
            CheckSurrounding();
        }

        private void CheckSurrounding()
        {
            playerInArea = Physics2D.OverlapBox(transform.position + positionOffset, detectorSize, 0, playerLayer);
            playerInHitArea = Physics2D.OverlapBox(hitBox.position + hitPositionOffset, hitDetectorSize, 0, playerLayer);
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                if (!playerInArea)
                {
                    Gizmos.color = gizmosColor;
                }
                else
                {
                    Gizmos.color = detectedColor;
                }
                Gizmos.DrawCube(transform.position + positionOffset, detectorSize);

                Gizmos.color = hitDetectedColor;
                Gizmos.DrawCube(hitBox.position + hitPositionOffset, hitDetectorSize);
            }
        }
    }
}
