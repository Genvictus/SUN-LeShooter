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
            GameObject[] enemyPets = GameObject.FindGameObjectsWithTag("EnemyPet");
            foreach (GameObject enemyPet in enemyPets)
            {
                EnemyPetHealth enemyPetHealth = enemyPet.GetComponent<EnemyPetHealth>();
                enemyPetHealth.TakeDamage(enemyPetHealth.currentHealth, enemyPet.transform.position);
            }
            
            return "Kill Pet Cheat Activated";
        }
    }
}
