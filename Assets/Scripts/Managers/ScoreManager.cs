using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;
        private static int levelThreshhold;
        const int LEVEL_INCREASE = 300;

        static Text sText;

        void Awake ()
        {
            sText = GetComponent <Text> ();

            score = 0;
            levelThreshhold = LEVEL_INCREASE;

            EventManager.StartListening("PlayerEarnScore", UpdateScore);
        }

        void OnDestroy()
        {
            EventManager.StopListening("PlayerEarnScore", UpdateScore);
        }

        public static void SetScore(int score)
        {
            ScoreManager.score = score;
        }

        void UpdateScore(int score)
        {
            IncreaseScore(score);
        }

        void Update ()
        {
            ProgressionManager.progressionState.score = score;
            sText.text = "Score: " + score;
            if (score >= levelThreshhold)
            {
                AdvanceLevel();
            }
        }

        public static void IncreaseScore(int newScore) {
            StatsManager.playerStats.scoreEarned += newScore;
            SetScore(score + newScore);
        }

        private static void AdvanceLevel()
        {
            levelThreshhold = score + LEVEL_INCREASE;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.AdvanceLevel();
        }
    }
}