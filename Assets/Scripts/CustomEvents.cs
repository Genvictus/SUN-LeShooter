using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public static void EnableShop()
    {
        Debug.Log("ini ketrigger");

        //kill all enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }

        EventManager.TriggerEvent("enableShop", true);
        EventManager.TriggerEvent("SpawnEnemy", false);
    }
    public static void DisableShop()
    {
        EventManager.TriggerEvent("enableShop", false);
        EventManager.TriggerEvent("SpawnEnemy", true);
    }
}
