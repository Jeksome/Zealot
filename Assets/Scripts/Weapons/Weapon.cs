using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon 
{
    public int damage;
    public int fireRate;
    public int ammo;

    public abstract void Shoot();
}
