using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class PetInvincibleCheat : Cheat
    {
        public PetInvincibleCheat()
        {
            cheatName = "Pet Invincible";
            cheatCode = "cumbehindme";
        }

        public override string ExecuteCheat()
        {
            GameObject[] pets = GameObject.FindGameObjectsWithTag("Pet");
            bool petCheatStatus = false;
            foreach (GameObject pet in pets)
            {
                PetHealth petHealth = pet.GetComponent<PetHealth>();
                petHealth.godMode = !petHealth.godMode;
                petCheatStatus = petHealth.godMode;
            }
            
            if (petCheatStatus) {
                return "Pet Invincible Cheat Activated";
            } else {
                return "Pet Invincible Cheat Deactivated";
            }
        }
    }
}
