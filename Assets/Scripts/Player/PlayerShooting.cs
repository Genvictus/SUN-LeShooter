using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Nightmare
{
    public class PlayerShooting : PausibleObject
    {

        public static Action shootInput;
        public static Action reloadInput;

        [SerializeField] private KeyCode reloadKey = KeyCode.R;

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
