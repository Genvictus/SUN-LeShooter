using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {

        public static Action shootInput;
        public static Action reloadInput;

        [SerializeField] private KeyCode reloadKey = KeyCode.R;
        public static float orbBuffMultiplier = 0.1f;
        public static int orbBuffCount = 0;
        public static float mobDebuff = 1;

        void Awake() {
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

        }

        void OnDestroy()
        {
            StopPausible();
        }
    }
}
