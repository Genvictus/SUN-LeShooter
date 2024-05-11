using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class InfiniteAmmoCheat : Cheat
    {
        public InfiniteAmmoCheat()
        {
            cheatName = "Infinite Ammo";
            cheatCode = "animobulet";
        }

        public override string ExecuteCheat()
        {
            PlayerShooting.infiniteBulletMode = !PlayerShooting.infiniteBulletMode;

            if (PlayerShooting.infiniteBulletMode) {
                return "Infinite Ammo Cheat Activated";
            } else {
                return "Infinite Ammo Cheat Deactivated";
            }
        }
    }
}
