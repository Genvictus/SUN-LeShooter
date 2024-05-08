using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("Level01", LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        // todo: load game
    }

    public void ViewStatistics()
    {
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
