using UnityEngine;

public class KillEnemiesQuestStep : QuestStep
{
    public int kerocoCount;
    public int jenderalCount;
    protected override void OnDisable()
    {
        QuestUI.RemoveDescription(questId);
        EventManager.StopListening("KerocoKilled", IncrementKeroco);
        EventManager.StopListening("Keroco(Clone)Killed", IncrementKeroco);
        EventManager.StopListening("JenderalKilled", IncrementJenderal);
        EventManager.StopListening("Jenderal(Clone)Killed", IncrementJenderal);
    }

    protected override void OnEnable()
    {
        EventManager.StartListening("KerocoKilled", IncrementKeroco);
        EventManager.StartListening("Keroco(Clone)Killed", IncrementKeroco);
        EventManager.StartListening("JenderalKilled", IncrementJenderal);
        EventManager.StartListening("Jenderal(Clone)Killed", IncrementJenderal);
    }

    private void Start() {
        QuestUI.AddDescription(questId, $"Kill {kerocoCount} kerocos and {jenderalCount} jenderals");
    }

    protected void IncrementKeroco()
    {
        kerocoCount--;
        CheckComplete();
    }

    protected void IncrementJenderal()
    {
        jenderalCount--;
        CheckComplete();
    }

    protected void CheckComplete()
    {

        if (kerocoCount <= 0 && jenderalCount <= 0)
        {
            FinishQuestStep();
        }
        else
        if (kerocoCount <= 0)
        {
            QuestUI.UpdateDescription(questId, $"Kill {jenderalCount} jenderals");
        }
        else
        if (jenderalCount <= 0)
        {
            QuestUI.UpdateDescription(questId, $"Kill {kerocoCount} kerocos");
        }
        else
        {
            QuestUI.UpdateDescription(questId, $"Kill {kerocoCount} kerocos and {jenderalCount} jenderals");
        }
    }
}