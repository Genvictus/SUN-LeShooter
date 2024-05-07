using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class PausibleObject : MonoBehaviour
    {
        public UnityAction<bool> pauseListener;
        internal bool isPaused = false;

        public void StartPausible()
        {
            pauseListener = new UnityAction<bool>(SetPause);

            EventManager.StartListening("Pause", SetPause);
        }

        public void StopPausible()
        {
            EventManager.StopListening("Pause", SetPause);
        }

        public void SetPause(bool state)
        {
            isPaused = state;
            if (isPaused)
            {
                OnPause();
            }
            else{
                OnUnPause();
            }
        }

        virtual public void OnPause()
        {

        }

        virtual public void OnUnPause()
        {

        }
    }
}