using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float missileSpeed = 10.0f;
    public int missileDamage = 1;
    void Start()
    {
        
    }

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
