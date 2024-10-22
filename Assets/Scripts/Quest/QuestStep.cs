using UnityEngine;

public abstract class QuestStep: MonoBehaviour
{
    protected bool isFinished = false;
    protected string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }

    protected virtual void FinishQuestStep()
    {
        isFinished = true;
        Debug.Log($"Quest step for {questId} completed!");
        QuestEvents.Instance.AdvanceQuest(questId);
        Destroy(gameObject);
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();
}