using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerHealthSystem : MonoBehaviour
    {
        private Animator anim;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthBar;

        void Start()
        {
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
        }
        private void Update()
        {
            healthBar.SetHealth(currentHealth);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            anim.SetTrigger("Hurt");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        public void SetMaxHealth()
        {
            currentHealth = maxHealth;
        }

        private void Die()
        {
            anim.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;

            this.enabled = false;
        }
    }
}
