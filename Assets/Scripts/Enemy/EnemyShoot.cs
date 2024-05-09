using UnityEngine;
using System.Collections;
using System;

namespace Nightmare
{
    public class EnemyShoot : EnemyAttack
    {
        public GunData gunData;

        protected override void Awake()
        {
            // Setting up the references.
            timeBetweenAttacks = gunData.fireRate;
            attackRange = gunData.maxDistance;
            base.Awake();
        }

        protected override Action<Transform> GetAttackAction()
        {
            return shootAction;
        }

        void OnDestroy()
        {
            StopPausible();
        }
    }
}