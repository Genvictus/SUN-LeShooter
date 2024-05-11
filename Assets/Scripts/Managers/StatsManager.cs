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
        playerStats.PlayTime += Time.deltaTime;
        Debug.Log("PlayerStats goldEarned: " + playerStats.goldEarned);
        Debug.Log("PlayerStats scoreEarned: " + playerStats.scoreEarned);
        Debug.Log("PlayerStats orbCollected: " + playerStats.orbCollected);
        Debug.Log("PlayerStats deathCount: " + playerStats.deathCount);

        Debug.Log("PlayerStats shotHit: " + playerStats.shotHit);
        Debug.Log("PlayerStats totalShot: " + playerStats.totalShot);
        Debug.Log("PlayerStats accuracy: " + playerStats.Accuracy);

    }
}
