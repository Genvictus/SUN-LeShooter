using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDamageAble
{
    void TakeDamage(float amount, Vector3 hitPoint);
}
