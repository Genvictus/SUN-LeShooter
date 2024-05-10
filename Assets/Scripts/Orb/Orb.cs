using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public abstract class Orb : MonoBehaviour
    {
        public float orbLifeTime = 5f;
        private static GameObject[] orbs = null;
        AudioSource pickupAudio;

        void Start()
        {
            pickupAudio = GetComponent<AudioSource>();
            Destroy(gameObject, orbLifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ApplyOrbEffect(other);
                pickupAudio.Play();
                enabled = false;
                Destroy(gameObject, pickupAudio.clip.length);
            }
        }

        public static GameObject[] GetOrbs()
        {
            orbs ??= new GameObject[] {
                    Resources.Load("HealOrb") as GameObject,
                    Resources.Load("AttackOrb") as GameObject,
                    Resources.Load("SpeedOrb") as GameObject
                };

            return orbs;
        }

        abstract public void ApplyOrbEffect(Collider other);
    }
}
