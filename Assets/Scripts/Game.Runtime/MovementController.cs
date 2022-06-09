using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class MovementController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator anim;
        public float moveSpeed = 5f;

        private float moveDir;
        private bool isFacingRight;
        private bool isWalking;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();    
        }

        private void Update()
        {
            CheckInput();
            CheckMovement();
            UpdateAnimation();
        }
        private void FixedUpdate()
        {
            ApplyMovement();
        }
        private void ApplyMovement()
        {
            rb.velocity = new Vector2 (moveDir * moveSpeed, rb.velocity.y);
        }
        private void CheckInput()
        {
            moveDir = Input.GetAxisRaw("Horizontal");
        }
        private void CheckMovement()
        {
            if (isFacingRight && moveDir > 0)
            {
                Flip();
            }
            else if (!isFacingRight && moveDir < 0)
            {
                Flip();
            }

            if (moveDir != 0)
            {
                isWalking = true;
            } else isWalking = false;
        }

        private void UpdateAnimation()
        {
            anim.SetBool("isWalking", isWalking);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0);
        }
    }
}
