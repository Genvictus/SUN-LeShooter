using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // TODO: persistent
    public static PlayerStats playerStats = new();

    void Start()
    {
        playerStats.InitialPlayTime ??= DateTimeOffset.Now.ToString();
    }

    void Update()
    {
        playerStats.PlayTime += TimeSpan.FromSeconds(Time.deltaTime);
        Debug.Log("PlayerStats cheatUsed: " + playerStats.cheatUsed);
        Debug.Log("PlayerStats distanceTraveled: " + playerStats.distanceTraveled);
    }
}
