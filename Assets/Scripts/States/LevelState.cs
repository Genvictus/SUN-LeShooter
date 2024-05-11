using System.Collections.Generic;

public class LevelState : SaveData<LevelState>
{
    public string currentPet;
    public Dictionary<string, bool> completedQuests;

    public Buffs buffs;
}

public struct Buffs
{
    public int damageBuffStack;
    public float speedBuffDuration;
}
