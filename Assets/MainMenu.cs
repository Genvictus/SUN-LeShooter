using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject savesMenu;
    public GameObject statisticsMenu;
    public GameObject settingsMenu;

    private bool overwriteSave = true;
    private const string SAVE_SLOT = "Save";

    void Start()
    {
        foreach (var item in savesMenu.gameObject.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (item.name.Equals("SaveSlot1"))
            {
                item.onClick.AddListener(delegate { StartSaveFile(1); });
            }
            else if (item.name.Equals("SaveSlot2"))
            {
                item.onClick.AddListener(delegate { StartSaveFile(2); });
            }
            else if (item.name.Equals("SaveSlot3"))
            {
                item.onClick.AddListener(delegate { StartSaveFile(3); });
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
    }

    public void LoadGame(bool overwriteSave)
    {
        this.overwriteSave = overwriteSave;
        DisableAllMenu();
        savesMenu.gameObject.SetActive(true);
        UpdateSaveSlotAvailability();
    }

    bool CheckSlotAvailability(int slotNum)
    {
        return PlayerPrefs.HasKey(ConstructSaveName(slotNum));
    }

    string GetSlotOwner(int slotNum)
    {
        return PlayerPrefs.GetString(ConstructSaveName(slotNum));
    }

    void SetSlotOwner(int slotNum, string playerName)
    {
        PlayerPrefs.SetString(ConstructSaveName(slotNum), playerName);
    }

    string ConstructSaveName(int slotNum)
    {
        return SAVE_SLOT + slotNum.ToString();
    }

    public void UpdateSaveSlotAvailability()
    {
        foreach (var item in savesMenu.gameObject.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (item.name.Equals("Back")) continue;

            int slotNum = int.Parse(item.name.Substring(item.name.Length - 1));

            // Check button interactability based on saved player name on slot
            if (CheckSlotAvailability(slotNum))
            {
                item.interactable = true;
                item.GetComponentInChildren<TMP_Text>().text = GetSlotOwner(slotNum);
            }
            else
            {
                item.interactable = false || overwriteSave;
                item.GetComponentInChildren<TMP_Text>().text = "Save Slot Empty";
            }
        }
    }

    public void StartSaveFile(int slotNum)
    {
        if (slotNum < 1 || slotNum > 3)
        {
            return;
        }

        string savefileName = ConstructSaveName(slotNum);
        string playerName = SavesHelper.playerName;

        // set active save file and set whether to load game or start new game in LevelManager
        LevelManager.SetSaveMetadata(savefileName, overwriteSave);
        if (overwriteSave)
        {
            // overwrite save slot name on UI, prepare folder for save
            SetSlotOwner(slotNum, playerName);
        }
        NewGame();

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

    public void ViewStatistics()
    {
        DisableAllMenu();
        statisticsMenu.gameObject.SetActive(true);

        PlayerStats stats;
        // init game stats file if none exists
        if (!SavesHelper.LoadPlayerStats(out stats))
        {
            // TODO load player stats
            stats = new();
            Debug.LogError("Unable to load player stats!");
            stats.SetInitialPlayTime();
            Debug.Log(stats.InitialPlayTime);
            Debug.Log(stats.PlayTime);

            SavesHelper.SavePlayerStats(stats);
        }

        // get text component for storing values on screen
        foreach (var tmp in statisticsMenu.transform.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if (tmp.name == "StatisticsValues")
            {
                tmp.text = string.Format(tmp.text,
                    stats.totalShot,
                    Math.Round(stats.Accuracy, 2),
                    stats.kerocoKillCount,
                    stats.kepalaKerocoKillCount,
                    stats.jenderalKillCount,
                    stats.rajaKillCount,
                    stats.increaseTortoiseKillCount,
                    stats.goldEarned,
                    stats.scoreEarned,
                    stats.orbCollected,
                    Math.Round(stats.distanceTraveled, 2),
                    stats.deathCount,
                    stats.cheatUsed,
                    TimeSpan.FromSeconds(stats.PlayTime));
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
