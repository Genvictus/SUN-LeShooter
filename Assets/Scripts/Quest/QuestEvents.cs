using System;

public class QuestEvents
{
    private static readonly QuestEvents instance = new();
    public static QuestEvents Instance => instance;

    public event Action<string> onStartQuest;
    public event Action<string> onAdvanceQuest;
    public event Action<string> onFinishQuest;
    public event Action<Quest> onQuestStateChange;

    private QuestEvents() {}

    public void StartQuest(string id)
    {
        if (onStartQuest is not null)
        {
            onStartQuest(id);
        }
    }

    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest is not null)
        {
            onAdvanceQuest(id);
        }
    }

    public void FinishQuest(string id)
    {
        if (onFinishQuest is not null)
        {
            onFinishQuest(id);
        }
    }

    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange is not null)
        {
            onQuestStateChange(quest);
        }
    }
}