using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class LevelManager : MonoBehaviour
    {
        private static bool isNewGame = false;
        private static string activeSaveName = "Save1";
        public string[] levels;

        private int currentLevel = 0;
        private Scene currentScene;
        private PlayerMovement playerMove;
        private Vector3 playerRespawn;
        private CinematicController[] cinema;
        private PauseManager pm;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        void Start()
        {
            cinema = FindObjectsOfType<CinematicController>().OrderBy(obj => obj.name).ToArray();

            foreach (var item in cinema)
            {
                Debug.Log(item.name);
            }

            LoadInitialLevel();
        }

        public static void SetSaveMetadata(string activeSave, bool startNewGame)
        {
            activeSaveName = activeSave;
            isNewGame = startNewGame;
            Debug.Log("Set Level Metadata, " + activeSave + startNewGame.ToString());
        }

        public void AdvanceLevel()
        {
            LoadLevel(currentLevel + 1);
        }

        public void LoadInitialLevel()
        {
            LoadLevel(0);
        }

        private void LoadLevel(int level)
        {
            Debug.Log("Loading Level " + level.ToString());

            currentLevel = level;

            playerMove = FindObjectOfType<PlayerMovement>();
            playerRespawn = playerMove.transform.position;
            pm = FindObjectOfType<PauseManager>();

            // order or setting pause matters for cursor lock
            pm.SetGameOverPause(false);
            pm.SetPause(false);

            string loadingScene = levels[level % levels.Length];
            SceneManager.LoadScene(loadingScene, LoadSceneMode.Additive);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;

            playerMove.transform.position = playerRespawn;
            SceneManager.SetActiveScene(scene);

            DisableOldScene();

            currentScene = scene;

            StartCutscene(0);
        }

        public void StartCutscene(int index)
        {
            cinema[index].StartCinematic(CinematicController.CinematicType.Realtime);
        }

        private void DisableOldScene()
        {
            if (currentScene.IsValid())
            {
                // Disable old scene.
                GameObject[] oldSceneObjects = currentScene.GetRootGameObjects();
                for (int i = 0; i < oldSceneObjects.Length; i++)
                {
                    oldSceneObjects[i].SetActive(false);
                }

                // Unload it.
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        void OnSceneUnloaded(Scene scene)
        {

        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}