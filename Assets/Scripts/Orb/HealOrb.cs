using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare {
    public class HealOrb : MonoBehaviour
    {
        public float orbLifeTime = 5f;
        public float healPercentage = 20f;

        void Start()
        {
            Destroy(gameObject, orbLifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                float healAmount = playerHealth.maxHealth * healPercentage / 100f;
                playerHealth.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
