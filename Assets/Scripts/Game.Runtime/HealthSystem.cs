using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class HealthSystem : MonoBehaviour
    {
        private Animator anim;
        public int maxHealth;
        public int currentHealth;

        public bool dropItem;

        public GameObject itemDrop;
        public Transform itemDropPoint;
        public GameObject healthBarObject;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
        }
        private void Update()
        {
            healthBarObject.GetComponent<HealthBar>().SetHealth(currentHealth);
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
            GetComponent<WaypointFollower>().enabled = false;
            GetComponent<AIOverlapDetector>().enabled = false;
            GetComponent<EnemyMovement>().enabled = false;
            healthBarObject.SetActive(false);
            this.enabled = false;

            if (dropItem)
            {
                Invoke("SpawnItem", 2f);
            }
        }

        private void SpawnItem()
        {
            Instantiate(itemDrop, itemDropPoint);
        }
    }
}
