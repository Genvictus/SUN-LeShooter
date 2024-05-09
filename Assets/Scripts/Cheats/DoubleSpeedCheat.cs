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

        public override void ExecuteCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerMovement playerMovement = player.GetComponent <PlayerMovement> ();
            playerMovement.godMode = !playerMovement.godMode;
            
            if (playerMovement.godMode) {
                Debug.Log("Player Speed Cheat Activated");
            } else {
                Debug.Log("Player Speed Cheat Deactivated");
            }
        }
    }
}
