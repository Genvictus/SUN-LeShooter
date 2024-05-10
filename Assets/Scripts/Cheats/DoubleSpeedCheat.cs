using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class DoubleSpeedCheat : Cheat
    {
        public DoubleSpeedCheat()
        {
            cheatName = "Double Speed";
            cheatCode = "fasterdadieh";
        }

        public override string ExecuteCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerMovement playerMovement = player.GetComponent <PlayerMovement> ();
            playerMovement.godMode = !playerMovement.godMode;
            
            if (playerMovement.godMode) {
                return "Player Speed Cheat Activated";
            } else {
                return "Player Speed Cheat Deactivated";
            }
        }
    }
}
