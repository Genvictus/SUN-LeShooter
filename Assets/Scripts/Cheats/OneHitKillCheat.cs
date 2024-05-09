using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class OneHitKillCheat : Cheat
    {
        public OneHitKillCheat()
        {
            cheatName = "One Hit Kill";
            cheatCode = "bigerwepong";
        }

        public override void ExecuteCheat()
        {
            PlayerShooting.godMode = !PlayerShooting.godMode;

            if (PlayerShooting.godMode) {
                Debug.Log("One Hit Kill Cheat Activated");
            } else {
                Debug.Log("One Hit Kill Cheat Deactivated");
            }
        }
    }
}
