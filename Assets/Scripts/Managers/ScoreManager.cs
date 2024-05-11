using UnityEngine;
using UnityEngine.UI;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;
        private int levelThreshhold;
        const int LEVEL_INCREASE = 300;

        Text sText;

        void Awake ()
        {
            sText = GetComponent <Text> ();

            score = 0;
            levelThreshhold = LEVEL_INCREASE;

            EventManager.StartListening("EnemyScore", UpdateScore);
        }

        void OnDestroy()
        {
            EventManager.StopListening("EnemyScore", UpdateScore);
        }

        void UpdateScore(int score)
        {
            ScoreManager.score += score;
        }

        void Update ()
        {
            sText.text = "Score: " + score;
            if (score >= levelThreshhold)
            {
                AdvanceLevel();
            }
        }

        private void AdvanceLevel()
        {
            levelThreshhold = score + LEVEL_INCREASE;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.AdvanceLevel();
        }
    }
}