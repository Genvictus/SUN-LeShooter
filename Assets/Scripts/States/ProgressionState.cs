using System;

public class ProgressionState : SaveData<ProgressionState>
{
    public string saveTime;

    public int coins;
    public double score;
    public int currentLevel;

    public Buffs buffs;

    public override string ToJson()
    {
        var now = DateTimeOffset.Now;
        saveTime = now.ToString();
        return base.ToJson();
    }
}

public struct Buffs
{
    public float damageBuff;
    // public float speedBuff;
}