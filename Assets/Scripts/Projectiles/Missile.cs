﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 10.0f;
    private int missileDamage = 5;

    void Update()
    {
        transform.Translate(0, 0, missileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hurt(missileDamage);
        }
        Destroy(this.gameObject);
    }
}
