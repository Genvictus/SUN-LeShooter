using System;
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
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject savesMenu;
    public GameObject statisticsMenu;
    public GameObject settingsMenu;

    private bool overwriteSave = true;
    private const string SAVE_SLOT = "saveslot";

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

    public void UpdateSaveSlotAvailability()
    {
        foreach (var item in savesMenu.gameObject.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (item.name.Equals("Back")) continue;

            int slotNum = int.Parse(item.name.Substring(item.name.Length - 1));
            string saveSlotName = SAVE_SLOT + slotNum.ToString();
            LevelState loadedSave;
            ProgressionState progressSave;
            if (SavesHelper.LoadLevelState(saveSlotName, out loadedSave) && SavesHelper.LoadProgressionState(saveSlotName, out progressSave))
            {
                item.interactable = true;
                item.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.HasKey(saveSlotName) ? PlayerPrefs.GetString(saveSlotName) : String.Format("Save Slot {0}", slotNum);
                // todo: show save slot information?
            }
            else
            {
                item.interactable = false || overwriteSave;
                item.GetComponentInChildren<TMP_Text>().text = "Save Slot Empty";
            }
        }
    }

    public void StartSaveFile(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 3)
        {
            return;
        }

        string savefileName = SAVE_SLOT + saveSlot.ToString();
        string playerName = SavesHelper.playerName;
        if (overwriteSave)
        {
            Debug.Log("New game overwrite save slot " + saveSlot.ToString());

            if (SavesHelper.CreateNewSave(savefileName))
            {
                Debug.Log("Success create new save file");
                PlayerPrefs.SetString(savefileName, playerName);
                NewGame();
            }
            else
            {
                Debug.LogError("Failed create save");
            }
        }
        else
        {
            Debug.Log("Load existing save slot " + saveSlot.ToString());

            LevelState loadedSave;
            ProgressionState progressSave;
            if (SavesHelper.LoadLevelState(savefileName, out loadedSave) && SavesHelper.LoadProgressionState(savefileName, out progressSave))
            {
                Debug.Log("Success load progression and save");
                // todo: continue to load level
            }
            else
            {
                Debug.LogError("Failed loading save");
            }
        }

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
