using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public string currentQuestId;

    void Start()
    {
        currentQuestId = "Level1";
        QuestEvents.Instance.StartQuest(currentQuestId);
    }
}