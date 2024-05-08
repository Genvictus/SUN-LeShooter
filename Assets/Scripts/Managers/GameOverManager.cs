using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class GameOverManager : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        Animator anim;

        LevelManager lm;
        PauseManager pm;
        private UnityEvent listener;
        private IEnumerator countdownCoroutine;

        void Awake()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent<Animator>();
            lm = FindObjectOfType<LevelManager>();
            pm = FindObjectOfType<PauseManager>();
            EventManager.StartListening("GameOver", ShowGameOver);
        }

        void OnDestroy()
        {
            EventManager.StopListening("GameOver", ShowGameOver);
        }

        void ShowGameOver()
        {
            Debug.Log("GameOver screen triggered");
            anim.SetBool("GameOver", true);
            pm.SetPause(true);
            pm.SetGameOverPause(true);

            countdownCoroutine = WaitAndReturnToMainMenu();
            StartCoroutine(countdownCoroutine);
            Debug.Log("Countdown started");
        }

        IEnumerator WaitAndReturnToMainMenu()
        {
            // animation for game over text is 15 seconds timer
            yield return new WaitForSeconds(15f);

            ExitToMainMenu();
        }

        void ExitToMainMenu()
        {
            Debug.Log("Return to main menu from game over");
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

        void StopCountdown()
        {
            StopCoroutine(countdownCoroutine);
            Debug.Log("Countdown interrupted");
        }
        public void ResetLevel()
        {
            // todo: fix restart game behaviour
            Debug.Log("Reset Level Triggered");

            StopCountdown();

            ScoreManager.score = 0;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.LoadInitialLevel();
            anim.SetBool("GameOver", false);
            pm.SetGameOverPause(false);
            pm.SetPause(false);

            playerHealth.ResetPlayer();
        }
    }
}