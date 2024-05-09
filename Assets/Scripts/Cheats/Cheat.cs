using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public abstract class Cheat
    {
        public string cheatName;
        public string cheatCode;
        public abstract void ExecuteCheat();
    }
}
