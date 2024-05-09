using System;
using UnityEngine;

public class PlayerStats : SaveData<PlayerStats>
{
    // Accuracy  stat
    public int totalshot;
    public int shotHit;
    public float Accuracy => shotHit / totalshot;

    // DistanceTraveled
    public float distanceTraveled;

    // play duration
    public string InitialPlayTime; // TODO update if needed/wanted
    public TimeSpan PlayTime => CalculatePlayTime();

    // TODO 3 more stats

    public void SetInitialPlayTime()
    {
        var now = DateTimeOffset.Now;
        InitialPlayTime ??= now.ToString();
    }

    private TimeSpan CalculatePlayTime()
    {
        if(InitialPlayTime is not null)
        {
            DateTimeOffset initialPlayTime = DateTimeOffset.Parse(InitialPlayTime);
            return DateTimeOffset.Now - initialPlayTime;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }
}