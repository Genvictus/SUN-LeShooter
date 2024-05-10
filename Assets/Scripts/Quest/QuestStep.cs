using UnityEngine;

public abstract class QuestStep: MonoBehaviour
{
    private bool isFinished = false;
    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }

    protected virtual void FinishQuestStep()
    {
        isFinished = true;
        QuestEvents.Instance.AdvanceQuest(questId);
        Destroy(gameObject);
    }

    protected abstract void OnEnable();
    protected abstract void OnDisable();
}