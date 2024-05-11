using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject player;
    public Image damageImage;
    public Slider healthSlider;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Color healthyColour = new Color(0.2f, 1f, 0.2f, 1f);
    public Color unhealthyColour = new Color(1f, 1f, 0f, 1f);
    public Color dyingColour = new Color(1f, 0f, 0f, 1f);
    public float flashSpeed = 5f;
    
    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        healthSlider.value = playerHealth.currentHealth;

        // If the player has just been damaged...
        if (playerHealth.damaged || playerHealth.currentHealth <= 0.25 * playerHealth.maxHealth)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Set health bar colour
        if (playerHealth.currentHealth >= 0.5 * playerHealth.maxHealth)
        {
            healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = healthyColour;
        }
        else if (playerHealth.currentHealth >= 0.25 * playerHealth.maxHealth)
        {
            healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = unhealthyColour;
        }
        else
        {
            healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = dyingColour;
        }

        // Reset the damaged flag.
        playerHealth.damaged = false;
    }
}
