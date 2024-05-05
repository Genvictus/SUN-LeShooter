using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Main");
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("Level01", LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        // todo: load game
    }

    public void ViewStatistics()
    {
        // todo: open statistic menu
    }

    public void ViewSettings()
    {
        // todo: open settings menu
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
