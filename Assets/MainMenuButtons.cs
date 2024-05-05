using System.Collections;
using System.Collections.Generic;
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
        Application.Quit();
    }
}
