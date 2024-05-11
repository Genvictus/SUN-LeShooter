using System;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    private static SavesManager instance;
    public static SavesManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SavesManager)) as SavesManager;
            }
            return instance;
        }
    }


    public LevelState LevelState;
    public ProgressionState ProgressionState;
    public PlayerStats PlayerStats;

    private PlayerGold playerGold;

    private string SaveName;

    public static void SelectSave(string saveName)
    {
        Instance.SaveName = saveName;
    }

    public static bool LoadSaves()
    {
        bool success = true;


        success = success && SavesHelper.LoadLevelState(Instance.SaveName, out Instance.LevelState);
        success = success && SavesHelper.LoadProgressionState(Instance.SaveName, out Instance.ProgressionState);
        success = success && SavesHelper.LoadPlayerStats(out Instance.PlayerStats);

        StatsManager.playerStats = Instance.PlayerStats;
        ProgressionManager.progressionState = Instance.ProgressionState;
        LevelStateManager.levelState = Instance.LevelState;

        return success;
    }

    public static void UpdateSaves()
    {
        Instance.PlayerStats = StatsManager.playerStats;
        Instance.ProgressionState = ProgressionManager.progressionState;
        Instance.LevelState = LevelStateManager.levelState;

        SavesHelper.CreateNewSave(Instance.SaveName);
        bool success = true;

        success = success && SavesHelper.SaveLevelState(Instance.SaveName, Instance.LevelState);
        success = success && SavesHelper.SaveProgressionState(Instance.SaveName, Instance.ProgressionState);
        success = success && SavesHelper.SavePlayerStats(Instance.PlayerStats);

        if (!success) 
        {
            Debug.LogError("Failed to save all player states");
        }
    }


    void Start()
    {
        SelectSave("Save1");
        LoadSaves();
        UpdateSaves();

        var player = GameObject.FindGameObjectWithTag("Player");
        EventManager.StartListening("SaveGameCompleted", UpdateSaves);
    }
}