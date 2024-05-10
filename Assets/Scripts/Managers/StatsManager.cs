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
        Debug.Log("PlayerStats kerocoKillCount: " + playerStats.kerocoKillCount);
        Debug.Log("PlayerStats kepalaKerocoKillCount: " + playerStats.kepalaKerocoKillCount);
        Debug.Log("PlayerStats jenderalKillCount: " + playerStats.jenderalKillCount);
        Debug.Log("PlayerStats rajaKillCount: " + playerStats.rajaKillCount);
        Debug.Log("PlayerStats increaseTortoiseKillCount: " + playerStats.increaseTortoiseKillCount);
    }
}
