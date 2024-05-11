using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {

        public Action shootInput;
        public Action reloadInput;

        [SerializeField] private KeyCode reloadKey = KeyCode.R;
        public static float orbBuffMultiplier = 0.1f;
        public static int orbBuffCount = 0;
        public static GameObject buffHUD = null;
        public static float mobDebuff = 1;
        public static bool godMode = false;
        public static bool infiniteBulletMode = false;

        void Awake()
        {
            StartPausible();
        }

        private void Update()
        {
            if (isPaused)
                return;

            if (Input.GetMouseButton(0))
                shootInput?.Invoke();

            if (Input.GetKeyDown(reloadKey))
                reloadInput?.Invoke();

            LevelStateManager.levelState.buffs.damageBuffStack = orbBuffCount;
        }

        void OnDestroy()
        {
            StopPausible();
        }

        public static float Calculatedamage(float initialDamage)
        {
            float damage = initialDamage;
            if (godMode)
            {
                damage = 6969.69f;
            }
            else
            {
                damage += initialDamage * orbBuffMultiplier * orbBuffCount * DifficultyManager.GetOrbBuffRate();
                damage *= mobDebuff;
                damage *= DifficultyManager.GetOutgoingDamageRate();
            }
            return damage;
        }
    }
}
