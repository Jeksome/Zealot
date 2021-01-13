using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int ammo;
    public int maxAmmo;
    public int weaponDamage;

    public float fireRate;
    public float hitForce;
    
    public abstract void Shoot();

}
