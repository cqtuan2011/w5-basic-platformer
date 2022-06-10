using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator anim;
        public Transform attackPoint;
        public float attackRange;
        public LayerMask enemyLayer;
        public int attackPower;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack_1");
            }
        }
        private void Attack() // set in event animation
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HealthSystem>().TakeDamage(attackPower);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
