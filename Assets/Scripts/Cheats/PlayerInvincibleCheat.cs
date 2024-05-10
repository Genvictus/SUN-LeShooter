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

        public override string ExecuteCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerHealth playerHealth = player.GetComponent <PlayerHealth> ();
            playerHealth.godMode = !playerHealth.godMode;
            
            if (playerHealth.godMode) {
                return "Player Invincible Cheat Activated";
            } else {
                return "Player Invincible Cheat Deactivated";
            }
        }
    }
}
