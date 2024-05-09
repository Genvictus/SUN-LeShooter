using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nightmare
{
    public class GetAllOrbsCheat : Cheat
    {
        public GetAllOrbsCheat()
        {
            cheatName = "Get All Orbs";
            cheatCode = "thankyousomuch";
        }

        public override string ExecuteCheat()
        {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");

            GameObject[] orbs = Orb.GetOrbs();
            Vector3 orbSpawnPosition = player.transform.position;
            orbSpawnPosition.y += 0.5f;
            foreach (GameObject orb in orbs)
            {
                GameObject.Instantiate(orb, orbSpawnPosition, Quaternion.identity);
            }

            return "Get All Orbs Effect Cheat Activated";
        }
    }
}
