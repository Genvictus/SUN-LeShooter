using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    void TakeDamage(int amount, Vector3 hitPoint);
}
