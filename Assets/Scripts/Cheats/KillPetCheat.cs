using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class KillPetCheat : Cheat
    {
        public KillPetCheat()
        {
            cheatName = "Kill Pet";
            cheatCode = "ambatu";
        }

        public override string ExecuteCheat()
        {
            // TODO
            return "Kill Pet Cheat Activated";
        }
    }
}
