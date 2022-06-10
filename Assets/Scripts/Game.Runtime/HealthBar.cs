using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public HealthSystem healthSystem;

        public Image fill;
        public Transform objectToFollow;
        public float healthBarOffset;
        private void Start()
        {
            if (healthSystem != null)
            {
                slider.maxValue = healthSystem.maxHealth;
            } else
            {
                return;
            }
        }

        private void Update()
        {
            transform.position = new Vector2(objectToFollow.position.x, objectToFollow.position.y + healthBarOffset);
        }

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = gradient.Evaluate(1f);
        }
        public void SetHealth(int health)
        {
            slider.value = health;

            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
