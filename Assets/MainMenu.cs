using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        // init game stats file if none exists
        if (!SavesManager.LoadPlayerStats(out stats))
        {
            stats = new();
            Debug.LogError("Unable to load player stats!");
            stats.SetInitialPlayTime();
            Debug.Log(stats.InitialPlayTime);
            Debug.Log(stats.PlayTime);

            SavesManager.SavePlayerStats(stats);
        }

        // get text component for storing values on screen
        foreach (var tmp in statisticsMenu.transform.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if (tmp.name == "StatisticsValues")
            {
                tmp.text = stats.totalshot.ToString() + "\n";
                tmp.text += stats.Accuracy.ToString() + "\n";
                tmp.text += stats.distanceTraveled.ToString() + "\n";
                tmp.text += stats.PlayTime.ToString() + "\n";

                // todo: update for new statistics 
            }
        }
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
