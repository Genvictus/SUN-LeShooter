using System;
using UnityEngine;

namespace Nightmare
{
  public class DifficultyManager : PausibleObject
  {
    public static int difficulty = 1;

    public static void SetDifficulty(int diff)
    {
      difficulty = Math.Max(Math.Min(2, diff), 0);
      Debug.Log("Set Difficulty : " + difficulty.ToString());
    }

    public static float GetIncomingDamageRate()
    {
      switch (difficulty)
      {
        case 0: return 0.8f;
        case 1: return 1f;
        case 2: return 1.6f;
        default: return 1f;
      }
    }
    public static float GetOutgoingDamageRate()
    {
      switch (difficulty)
      {
        case 0: return 1.75f;
        case 1: return 1f;
        case 2: return 0.75f;
        default: return 1f;
      }
    }
    public static float GetPetPrice()
    {
      if (difficulty == 0)
      {
        return 0.75f;
      }
      else
      {
        return difficulty * 1f;
      }
    }
    public static float GetOrbBuffRate()
    {
      return 1f + (difficulty - 1) * 0.25f;
    }
    public static float GetEnemySpawnRate()
    {
      return 1f + difficulty * 0.5f;
    }
  }
}