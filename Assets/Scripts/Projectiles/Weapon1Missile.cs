using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Missile : MonoBehaviour
{
    public float missileSpeed = 200.0f;
    public int missileDamage = 1;
    void Update()
    {
        transform.Translate(0, 0, missileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PatrolingBot bot = other.GetComponent<PatrolingBot>();
        if (bot != null)
        {
            bot.Hurt(missileDamage);
        }
        Destroy(this.gameObject);
    }
}
