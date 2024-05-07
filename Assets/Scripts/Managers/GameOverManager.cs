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
        private UnityEvent listener;

        void Awake()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            anim = GetComponent<Animator>();
            lm = FindObjectOfType<LevelManager>();
            EventManager.StartListening("GameOver", ShowGameOver);
            EventManager.StartListening("ReturnToMainMenu", ShowGameOver);
        }

        void OnDestroy()
        {
            EventManager.StopListening("GameOver", ShowGameOver);
            EventManager.StopListening("ReturnToMainMenu", ShowGameOver);
        }

        void ShowGameOver()
        {
            Debug.Log("GameOver screen triggered");
            anim.SetBool("GameOver", true);

            StartCoroutine(WaitAndReturnToMainMenu());
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
        private void ResetLevel()
        {
            ScoreManager.score = 0;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.LoadInitialLevel();
            anim.SetBool("GameOver", false);
            playerHealth.ResetPlayer();
        }
    }
}