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
        public float jumpForce = 3f;
        public int jumpPower;

        public Transform groundCheckPoint;
        public float groundCheckSize;
        public LayerMask groundLayer;

        private float moveDir;
        private bool isFacingRight;
        private bool isWalking;
        private bool isJumping;
        private bool canJump;
        private bool isGrounded;

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
            CheckSurrounding();
            Jump();
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

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }

        private void CheckSurrounding()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckSize, groundLayer);
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
        private void Jump()
        {
            if (canJump)
            {
                isJumping = true;
                rb.velocity = new Vector2 (rb.velocity.x, jumpForce * jumpPower);
                anim.SetTrigger("Jump_2");
            } else
            {
                isJumping = false;
            }
        }

        private void UpdateAnimation()
        {
            anim.SetBool("isWalking", isWalking);
            //anim.SetBool("isJumping", isJumping);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckSize);
        }
    }
}
