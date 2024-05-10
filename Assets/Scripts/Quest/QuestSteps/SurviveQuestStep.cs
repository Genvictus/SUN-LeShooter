using System.Collections;
using System.Threading;
using UnityEngine;

public class SurviveQuestStep : QuestStep
{
    private float timeToComplete = 60;
    private Coroutine timer;

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(timeToComplete);
        FinishQuestStep();
    }

    protected override void OnEnable()
    {
        timer = StartCoroutine(Wait());
    }

    protected override void OnDisable()
    {
        StopCoroutine(timer);
    }
}