using System;
using UnityEngine;

public class PlayerStats : SaveData<PlayerStats>
{
    // Accuracy  stat
    public int totalShot; // TODO
    public int shotHit; // TODO
    public float Accuracy => totalShot == 0 ? 0 : shotHit / totalShot;

    // KIll count
    public int kerocoKillCount; // TODO
    public int kepalaKerocoKillCount; // TODO
    public int jendralKillCount; // TODO
    public int rajaKillCount; // TODO
    public int increaseTortoiseKillCount; // TODO

    // Cheat
    public int cheatUsed; // TODO

    // DistanceTraveled
    public float distanceTraveled; // TODO

    // play duration
    public string InitialPlayTime;
    public TimeSpan PlayTime;

    public void SetInitialPlayTime()
    {
        InitialPlayTime ??= DateTimeOffset.Now.ToString();
    }
}