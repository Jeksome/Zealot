using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firemace : Weapon
{
    int fire;
    public override void Shoot()
    {
        fire = damage * fireRate;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
