using System;
using UnityEngine;

public class SavesManager
{
    private static string playerKey = "player";
    public static string playerName => PlayerPrefs.HasKey(playerKey) ? PlayerPrefs.GetString(playerKey) : "Player";

    private const string PROGRESSION = "progression";
    private const string LEVEL = "level";
    private const string PLAYERSTATS = "playerstats";

    public static void SetPlayerName(string name)
    {
        PlayerPrefs.SetString(playerKey, name);
    }

    public static bool LoadPlayerStats(out PlayerStats playerStats)
    {
        playerStats = new();

        bool status = SaveFileManager.LoadFromFile(PLAYERSTATS, out string statsJSON);
        if (!status) return false;

        playerStats.LoadFromJson(statsJSON);
        return true;
    }

    public static bool SavePlayerStats(PlayerStats playerStats)
    {
        string statsJSON = playerStats.ToJson();
        return SaveFileManager.WriteToFile(PLAYERSTATS, statsJSON);
    }

    public static bool CreateNewSave(string saveName)
    {
        return SaveFileManager.CreateDirectory(saveName);
    }

    public static bool ChangeSaveName(string newSaveName, string oldSaveName)
    {
        if (SaveFileManager.RenameDirectory(oldSaveName, newSaveName))
        {
            Debug.LogError($"Error renaming save!");
            return false;
        }
        return true;
    }

    public static bool LoadProgressionState(string saveName, out ProgressionState progression)
    {
        progression = new();
        return SaveManager<ProgressionState>.LoadSave(saveName, PROGRESSION, progression);
    }

    public static bool SaveProgressionState(string saveName, ProgressionState progression)
    {
        progression.UpdateSaveTime();
        return SaveManager<ProgressionState>.Save(saveName, PROGRESSION, progression);
    }

    public static bool LoadLevelState(string saveName, out LevelState levelState)
    {
        levelState = new();
        return SaveManager<LevelState>.LoadSave(saveName, LEVEL, levelState);
    }

    public static bool SaveLevelState(string saveName, LevelState levelState)
    {
        return SaveManager<LevelState>.Save(saveName, LEVEL, levelState);
    }
}