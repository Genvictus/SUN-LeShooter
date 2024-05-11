using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    private int currentQuestIdx;

    private static readonly string[] QuestOrder = {
        "Level1",
        "Level2"
    };

    void Start()
    {
        currentQuestIdx = 0;
        QuestEvents.Instance.StartQuest(QuestOrder[currentQuestIdx]);

        EventManager.StartListening("QuestComplete", AdvanceQuest);
    }

    void OnDestroy()
    {
        EventManager.StopListening("QuestComplete", AdvanceQuest);
    }

    void AdvanceQuest()
    {
        currentQuestIdx++;
        QuestEvents.Instance.StartQuest(QuestOrder[currentQuestIdx]);
    }

    public static void AwardPlayer()
    {
        EventManager.TriggerEvent("PlayerEarnGold", 69);
        EventManager.TriggerEvent("PlayerEarnScore", 69);
    }
}