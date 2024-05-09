using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nightmare {
    public class AttackOrb : Orb
    {
        public int maxBuffCount = 15;
        public override void ApplyOrbEffect(Collider other)
        {
            Debug.Log("Player got attack buff");
            if (PlayerShooting.orbBuffCount == 0) {
                GameObject buffHUD = Resources.Load("AttackBuffHUD") as GameObject;
                PlayerShooting.buffHUD = BuffManager.AddBuff(buffHUD);
            }
            
            if (PlayerShooting.orbBuffCount < maxBuffCount) {
                PlayerShooting.orbBuffCount++;
                Text stackText = PlayerShooting.buffHUD.GetComponentInChildren<Text>();
                stackText.text = PlayerShooting.orbBuffCount.ToString();
            }
        }
    }
}
