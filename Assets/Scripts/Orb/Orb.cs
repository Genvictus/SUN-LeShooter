using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public abstract class Orb : MonoBehaviour
    {
        public float orbLifeTime = 5f;
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

        abstract public void ApplyOrbEffect(Collider other);
    }
}
