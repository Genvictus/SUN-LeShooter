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

        return success;
    }

    public static bool UpdateSaves()
    {
        SavesHelper.CreateNewSave(Instance.SaveName);
        bool success = true;

        success = success && SavesHelper.SaveLevelState(Instance.SaveName, Instance.LevelState);
        success = success && SavesHelper.SaveProgressionState(Instance.SaveName, Instance.ProgressionState);
        success = success && SavesHelper.SavePlayerStats(Instance.PlayerStats);

        return success;
    }


    void Start()
    {
        SelectSave("Save1");
        LoadSaves();
        UpdateSaves();
    }
}