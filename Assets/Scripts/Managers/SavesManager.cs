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

    private LevelState levelState;
    private ProgressionState progressionState;
    private PlayerStats playerStats;

    public LevelState LevelState => levelState;
    public ProgressionState ProgressionState => progressionState;
    public PlayerStats PlayerStats => playerStats;

    private string SaveName;

    public static void SelectSave(string saveName)
    {
        Instance.SaveName = saveName;
    }

    public static bool LoadSaves()
    {
        bool success = true;

        success = success && SavesHelper.LoadLevelState(Instance.SaveName, out Instance.levelState);
        success = success && SavesHelper.LoadProgressionState(Instance.SaveName, out Instance.progressionState);
        success = success && SavesHelper.LoadPlayerStats(out Instance.playerStats);

        return success;
    }

    public static bool UpdateSaves()
    {
        SavesHelper.CreateNewSave(Instance.SaveName);
        bool success = true;

        success = success && SavesHelper.SaveLevelState(Instance.SaveName, Instance.levelState);
        success = success && SavesHelper.SaveProgressionState(Instance.SaveName, Instance.progressionState);
        success = success && SavesHelper.SavePlayerStats(Instance.playerStats);

        return success;
    }


    void Start()
    {
        SelectSave("Save1");
        LoadSaves();
        UpdateSaves();
    }
}