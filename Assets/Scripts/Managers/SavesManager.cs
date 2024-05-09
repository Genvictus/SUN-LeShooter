using System;
using UnityEngine;

public class SavesManager
{
    public string saveName;

    private const string PROGRESSION = "progression";
    private const string LEVEL = "level";
    private const string PLAYERSTATS = "playerstats";

    public static bool LoadPlayerStats(out PlayerStats playerStats)
    {
        playerStats = new();

        bool status = SaveFileManager.LoadFromFile(PLAYERSTATS, out string statsJSON);
        if(!status) return false;

        playerStats.LoadFromJson(statsJSON);
        return true;
    }

    public static bool SavePlayerStats(PlayerStats playerStats)
    {
        string statsJSON = playerStats.ToJson();
        return SaveFileManager.WriteToFile(PLAYERSTATS, statsJSON);
    }

    public bool CreateNewSave(string saveName)
    {
        return SaveFileManager.CreateDirectory(saveName);
    }

    public bool ChangeSaveName(string saveName)
    {
        string oldSaveName = this.saveName;
        if(SaveFileManager.RenameDirectory(oldSaveName, saveName))
        {
            this.saveName = saveName;
            Debug.LogError($"Error renaming save!");
            return false;
        }
        return true;
    }

    public bool LoadProgressionState(out ProgressionState progression)
    {
        progression = new();
        return SaveManager<ProgressionState>.LoadSave(saveName, PROGRESSION, progression);
    }

    public bool SaveProgressionState(ProgressionState progression)
    {
        progression.UpdateSaveTime();
        return SaveManager<ProgressionState>.Save(saveName, PROGRESSION, progression);
    }

    public bool LoadLevelState(out LevelState levelState)
    {
        levelState = new();
        return SaveManager<LevelState>.LoadSave(saveName, LEVEL, levelState);
    }

    public bool SaveLevelState(LevelState levelState)
    {
        return SaveManager<LevelState>.Save(saveName, LEVEL, levelState);
    }
}