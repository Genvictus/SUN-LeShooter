using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nightmare;


public class PetHolder : PausibleObject
{
    // Start is called before the first frame update
    int petCount;

    object weapon;
    EnemyMelee melee;
    EnemyGun gun;

    public Transform target;

    float timer;
    void Awake()
    {
        StartPausible();
    }
    void OnDestroy()
    {
        StopPausible();
    }

    void Start()
    {
        petCount = transform.childCount;
        melee = target.GetComponent<EnemyMelee>();
        if (weapon == null)
        {
            gun = target.GetComponent<EnemyGun>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isPaused || isGameOver)
            return;

        timer += Time.deltaTime;
        petCount = transform.childCount;

        if (timer >= 0.2f)
        {
            Debug.Log("petCount" + petCount);
            if (melee != null)
            {
                melee.buffCount = petCount;
            } else
            {
                gun.buffCount = petCount;
            }
        }
    }

}
