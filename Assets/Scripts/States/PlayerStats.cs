using System;
using UnityEngine;

public class PlayerStats : SaveData<PlayerStats>
{
    // Accuracy  stat
    public int totalShot;
    public int shotHit;
    public float Accuracy => totalShot == 0 ? 0 : shotHit / totalShot;

    // KIll count
    public int kerocoKillCount;
    public int kepalaKerocoKillCount;
    public int jenderalKillCount;
    public int rajaKillCount;
    public int increaseTortoiseKillCount;

    // Earned
    public int goldEarned; // TODO
    public int scoreEarned; // TODO
    public int orbCollected; // TODO
    public float distanceTraveled;

    // Count
    public int deathCount; // TODO
    public int cheatUsed;

    // play duration
    public string InitialPlayTime;
    public TimeSpan PlayTime;

    public void SetInitialPlayTime()
    {
        InitialPlayTime ??= DateTimeOffset.Now.ToString();
    }
}