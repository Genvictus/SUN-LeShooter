using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class PlayerInvincibleCheat : Cheat
    {
        public PlayerInvincibleCheat()
        {
            cheatName = "Player Invincible";
            cheatCode = "iwillprotectyou";
        }

        public override void ExecuteCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerHealth playerHealth = player.GetComponent <PlayerHealth> ();
            playerHealth.godMode = !playerHealth.godMode;
            
            if (playerHealth.godMode) {
                Debug.Log("Player Invincible Cheat Activated");
            } else {
                Debug.Log("Player Invincible Cheat Deactivated");
            }
        }
    }
}
