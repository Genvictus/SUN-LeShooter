using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject savesMenu;
    public GameObject statisticsMenu;
    public GameObject settingsMenu;

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
    }

    private void DisableAllMenu()
    {
        mainMenu.gameObject.SetActive(false);
        savesMenu.gameObject.SetActive(false);
        statisticsMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
    }

    public void BackToMain()
    {
        DisableAllMenu();
        mainMenu.gameObject.SetActive(true);
    }

    public void LoadGame()
    {
        DisableAllMenu();
        savesMenu.gameObject.SetActive(true);

        // todo: load game
    }

    public void ViewStatistics()
    {
        DisableAllMenu();
        statisticsMenu.gameObject.SetActive(true);

        PlayerStats stats;
        // TODO: this is just loading and saving PlayerStats template
        if (!SavesManager.LoadPlayerStats(out stats))
        {
            stats = new();
            Debug.LogError("Unable to load player stats!");
        }

        stats.SetInitialPlayTime();
        Debug.Log(stats.InitialPlayTime);
        Debug.Log(stats.PlayTime);

        SavesManager.SavePlayerStats(stats);
    }

    public void ViewSettings()
    {
        DisableAllMenu();
        settingsMenu.gameObject.SetActive(true);
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
