using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class ItemDrop : MonoBehaviour
    {
        public bool healthBuff;
        public bool jumpBuff;
        public bool attackBuff;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (healthBuff)
                {
                    collision.GetComponent<PlayerHealthSystem>().SetMaxHealth();
                } else if (jumpBuff)
                {
                    collision.GetComponent<MovementController>().jumpPower++;
                } else if (attackBuff)
                {
                    collision.GetComponent<PlayerAttack>().attackPower++;
                }
                Destroy(gameObject);
            }
        }
    }
}
