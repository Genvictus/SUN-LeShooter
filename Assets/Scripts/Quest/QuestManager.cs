using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    void Awake()
    {
        InitializeQuestMap();
    }

    void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            QuestEvents.Instance.QuestStateChange(quest);
        }
    }

    void OnEnable()
    {
        QuestEvents.Instance.onStartQuest += StartQuest;
        QuestEvents.Instance.onAdvanceQuest += AdvanceQuest;
        QuestEvents.Instance.onFinishQuest += FinishQuest;
    }

    void OnDisable()
    {
        QuestEvents.Instance.onStartQuest -= StartQuest;
        QuestEvents.Instance.onAdvanceQuest -= AdvanceQuest;
        QuestEvents.Instance.onFinishQuest -= FinishQuest;
    }

    private void InitializeQuestMap()
    {
        QuestInfoSO[] questInfos = Resources.LoadAll<QuestInfoSO>("Quests");

        questMap = new();
        foreach (QuestInfoSO questInfo in questInfos)
        {
            if (questMap.ContainsKey(questInfo.ID))
            {
                Debug.LogWarning($"Duplicate quest ID detected: {questInfo.ID}");
            }
            questMap.Add(questInfo.ID, new Quest(questInfo));
        }
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        foreach (QuestInfoSO prereqInfo in quest.info.questPrerequisites)
        {
        Quest prereqQuest = GetQuestById(prereqInfo.ID);
            meetsRequirements = meetsRequirements && (prereqQuest.state != QuestState.Finished);
        }

        return meetsRequirements;
    }

    public void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(transform);
        ChangeQuestState(id, QuestState.InProgress);
    }

    public void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.AdvanceQuestStep();
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(transform);
        }
        else
        {
            // No more quest steps: quest can be finished
            if(quest.info.automaticallyComplete)
            {
                FinishQuest(id);
                if(quest.info.automaticallyComplete)
                {
                    foreach(var nextQuest in quest.info.questToStart)
                    {
                        StartQuest(nextQuest.ID);
                    }
                }
            }
            else
            {
                ChangeQuestState(id, QuestState.CanFinish);
            }
        }
    }

    public void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.TriggerCompletion();
        ChangeQuestState(id, QuestState.Finished);
        foreach (Quest q in questMap.Values)
        {
            if (q.state == QuestState.RequirementsNotMet)
            {
                if (CheckRequirementsMet(q))
                {
                    ChangeQuestState(q.info.ID, QuestState.CanStart);
                }
            }
        }
    }


    public void RequirementsChange()
    {

    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        QuestEvents.Instance.QuestStateChange(quest);
    }

    private Quest GetQuestById(string id)
    {
        return questMap[id];
    }
}