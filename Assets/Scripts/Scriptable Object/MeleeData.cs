using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Melee", menuName ="Weapon/Melee")]
public class MeleeData : ScriptableObject
{

    public new string name;
    public float damage;
    public float originalDamage;

    public float maxDistance;
    public float fireRate;

    [HideInInspector]
    public bool attacking;
}
