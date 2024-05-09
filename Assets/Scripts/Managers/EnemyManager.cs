using UnityEngine;

namespace Nightmare
{
    public class EnemyManager : PausibleObject
    {
        private PlayerHealth playerHealth;
        public GameObject enemy;
        public float spawnTime = 3f;
        public Transform[] spawnPoints;

        private float timer;
        private int spawned = 0;

        void Start ()
        {
            timer = spawnTime;
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        void OnEnable()
        {
            StartPausible();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void Update()
        {
            if (isPaused)
                return;

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Spawn();
                spawned += 1;
                timer = spawnTime;
            }
        }

        void Spawn ()
        {
            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            
            var instantiatedEnemy = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            if (enemy.name == "Jenderal" || enemy.name == "Raja" || enemy.name == "Jenderal(Clone)" || enemy.name == "Raja(Clone)")
            {
                var pet = Resources.Load("IncreaseTortoise") as GameObject;
                var petObject = Instantiate(pet, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
                var petScript = petObject.GetComponent<IncreaseTortoise>();
                petScript.followTarget = instantiatedEnemy;
            }
            return;
        }
    }
}