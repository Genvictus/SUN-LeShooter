using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDamageAble
{
    void TakeDamage(int amount, Vector3 hitPoint);
}
