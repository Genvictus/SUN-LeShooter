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

    public static int GetPlayerHealthCap()
    {
      return 1;
      // todo: calculate based on difficulty;
    }

    public static float GetEnemyFireRate()
    {
      return 1;
      // todo: calculate based on difficulty;
    }

    public static float GetEnemySpawnRate()
    {
      return 1;
      // todo: calculate based on difficulty;
    }
  }
}