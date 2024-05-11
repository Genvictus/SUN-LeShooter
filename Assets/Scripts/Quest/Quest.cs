using UnityEngine;

public class Quest
{
    [SerializeField] public QuestState state;
    [SerializeField] int currentQuestStepIndex;

    public QuestInfoSO info;
    public int CurrentStep => currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        info = questInfo;
        state = QuestState.RequirementsNotMet;
        currentQuestStepIndex = 0;
    }

    public void AdvanceQuestStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < info.questStepPrefabs.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab is not null)
        {
            QuestStep questStep = GameObject.Instantiate(questStepPrefab, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.ID);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if (CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning($"No more quest step: QuestId = {info.ID}, index = {currentQuestStepIndex}");
        }
        return questStepPrefab;
    }

    public void TriggerCompletion()
    {
        info.finishingEvent.Invoke();
    }
}

public enum QuestState
{
    RequirementsNotMet,
    CanStart,
    InProgress,
    CanFinish,
    Finished
}