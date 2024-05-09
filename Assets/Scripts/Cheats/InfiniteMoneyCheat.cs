using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class InfiniteMoneyCheat : Cheat
    {
        public InfiniteMoneyCheat()
        {
            cheatName = "Infinite Money";
            cheatCode = "akutubuhpeluru";
        }

        public override void ExecuteCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            PlayerGold playerGold = player.GetComponent <PlayerGold> ();
            playerGold.goldAmount = 69696969;
            playerGold.godMode = true;
            Debug.Log("Infinite Money Cheat Activated");
        }
    }
}
