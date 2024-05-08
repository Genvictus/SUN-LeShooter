using UnityEngine;
using UnityEngine.Events;

namespace Nightmare
{
    public class PausibleObject : MonoBehaviour
    {
        public UnityAction<bool> pauseListener;
        internal bool isPaused = false;
        internal bool isGameOver = true;

        public void StartPausible()
        {
            pauseListener = new UnityAction<bool>(SetPause);

            EventManager.StartListening("Pause", SetPause);
            EventManager.StartListening("GameOverPause", SetGameOver);
        }

        public void StopPausible()
        {
            EventManager.StopListening("Pause", SetPause);
            EventManager.StopListening("GameOverPause", SetGameOver);
        }

        public void SetPause(bool state)
        {
            isPaused = state;
            if (isPaused)
            {
                OnPause();
            }
            else
            {
                OnUnPause();
            }
        }

        public void SetGameOver(bool state)
        {
            isGameOver = state;
            if (isGameOver)
            {
                OnPause();
            }
            else
            {
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