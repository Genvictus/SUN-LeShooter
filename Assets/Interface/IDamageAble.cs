using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    void TakeDamage(float damage, Vector3 hitPoint);
}
