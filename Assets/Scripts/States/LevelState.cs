using System.Collections.Generic;

public class LevelState : SaveData<LevelState>
{
    public string currentPet;
    public Dictionary<string, bool> completedQuests;
}
