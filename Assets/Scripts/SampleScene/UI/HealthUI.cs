using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;

    public void UpdateHealth(float playerHealth)
    {
        Debug.Log("Player Health: " + playerHealth);
        int health = Mathf.RoundToInt(playerHealth);
        healthText.text = health.ToString();
    }
}