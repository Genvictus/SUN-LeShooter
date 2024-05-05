using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Melee", menuName ="Weapon/Melee")]
public class Melee : ScriptableObject
{

    public new string name;
    public float damage;
    public float maxDistance;
    public float fireRate;
}
