using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare {
    public class HealOrb : Orb
    {
        public float healPercentage = 0.2f;
        public override void ApplyOrbEffect(Collider other)
        {
            Debug.Log("Player got heal orb");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            float healAmount = playerHealth.maxHealth * healPercentage;
            playerHealth.Heal(healAmount);
        }
    }
}
