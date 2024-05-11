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

        public override string ExecuteCheat()
        {
            PlayerShooting.godMode = !PlayerShooting.godMode;

            if (PlayerShooting.godMode) {
                return "One Hit Kill Cheat Activated";
            } else {
                return "One Hit Kill Cheat Deactivated";
            }
        }
    }
}
