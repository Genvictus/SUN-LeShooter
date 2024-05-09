using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare {
    public class SpeedOrb : Orb
    {
        public float buffDuration = 15f;
        public override void ApplyOrbEffect(Collider other)
        {
            Debug.Log("Player got speed buff");
            if (PlayerMovement.buffHUD is null) {
                GameObject buffHUD = Resources.Load("SpeedBuffHUD") as GameObject;
                PlayerMovement.buffHUD = BuffManager.AddBuff(buffHUD);
            }
            
            PlayerMovement.speedBuffTimer = buffDuration;
            PlayerMovement.setSpeedDurationText(buffDuration);
        }
    }
}
