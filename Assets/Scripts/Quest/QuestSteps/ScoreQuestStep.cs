using System.Diagnostics.Tracing;

public class ScoreQuestStep : QuestStep
{
  public int requiredScore = 150;
  protected override void OnDisable()
  {
    EventManager.StopListening("PlayerEarnScore", IncrementScore);
  }

  protected override void OnEnable()
  {
    EventManager.StartListening("PlayerEarnScore", IncrementScore);
  }

  private void IncrementScore(int score)
  {
    requiredScore -= score;
    if (requiredScore <= 0)
    {
      FinishQuestStep();
    }
  }
}