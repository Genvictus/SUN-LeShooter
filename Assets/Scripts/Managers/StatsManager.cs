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
        Debug.Log("PlayerStats totalShot: " + playerStats.totalShot);
        Debug.Log("PlayerStats shotHit: " + playerStats.shotHit);
    }
}
