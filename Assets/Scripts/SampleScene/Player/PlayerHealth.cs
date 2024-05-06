using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IMobs, IDamageAble
{
    public HealthUI healthUI;
    public GameEndUI gameEndUI;

    public float Health { get; set; }

    void Start()
    {
        Health = 69;

        healthUI = GameObject.FindGameObjectWithTag("UI").GetComponent<HealthUI>();
        gameEndUI = GameObject.FindGameObjectWithTag("UI").GetComponent<GameEndUI>();
        healthUI.UpdateHealth(Health);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthUI.UpdateHealth(Health);

        if (Health <= 0)
            gameEndUI.EndGame("You Lose");
    }
    public void Damage(float damage)
    {
        Health -= damage;
        healthUI.UpdateHealth(Health);

        if (Health <= 0)
            gameEndUI.EndGame("You Lose");
    }
    
}
