using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int totalEnemies;
    public GameEndUI gameEndUI;

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        gameEndUI = GameObject.FindGameObjectWithTag("UI").GetComponent<GameEndUI>();
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
            gameEndUI.EndGame("You Win");
    }
}
