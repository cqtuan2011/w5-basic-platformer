using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class HealthSystem : MonoBehaviour
    {
        private Animator anim;
        public bool isFinalBoss;
        public int maxHealth;
        public int currentHealth;

        public bool dropItem;
        public float itemSpawnTime = 1.5f;

        public GameObject victoryPanel;

        public GameObject[] itemDropList;
        public Transform[] itemDropPoint;
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
                Invoke("SpawnItem", itemSpawnTime);
            }

            if (isFinalBoss)
            {
                victoryPanel.SetActive(true);
            }
        }

        private void SpawnItem()
        {
            for (int i = 0; i < itemDropList.Length; i++)
            {
                Instantiate(itemDropList[i], itemDropPoint[i]);
            }
        }
    }
}
