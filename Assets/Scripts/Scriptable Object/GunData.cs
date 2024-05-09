using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{

    public new string name;

    public float damage;
    public float originalDamage;
    public float maxDistance;

    public int currentAmmo;
    public int magSize;

    public float reloadTime;

    public float fireRate;

    public bool spread;
    [HideInInspector]
    public bool reloading;
    [HideInInspector] 
    public bool shooting;

}
