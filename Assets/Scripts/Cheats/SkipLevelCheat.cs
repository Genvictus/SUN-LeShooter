using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class SkipLevelCheat : Cheat
    {
        public SkipLevelCheat()
        {
            cheatName = "Skip Level";
            cheatCode = "skipkelas";
        }

        public override string ExecuteCheat()
        {
            // TODO
            return "Skip Level Cheat Activated";
        }
    }
}
