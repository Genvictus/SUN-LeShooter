using System;

public class ProgressionState : SaveData<ProgressionState>
{
    public string SaveTime { get; private set; }

    public int coins;
    public double score;
    public int currentLevel;

    public void UpdateSaveTime(){
        var now = DateTimeOffset.Now;
        SaveTime = now.ToString();
    }
}