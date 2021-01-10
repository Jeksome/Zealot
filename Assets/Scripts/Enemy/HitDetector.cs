using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    //[SerializeField] private GameObject _rotEffectParticle;
    public GameObject hitEffect;
    public void HitReaction(Vector3 pos)
    {
        PatrolingBot Zombie = GetComponent<PatrolingBot>();
        if (Zombie != null)
        {
            //Zombie.SetAlive(false);
            Zombie.Hurt(1);
            Bleeding(pos);
        }
        //StartCoroutine(BloodSpray(pos));
    }

    void Bleeding(Vector3 pos)
    {
        GameObject spawnedDecal = GameObject.Instantiate(hitEffect, pos, Quaternion.LookRotation(pos.normalized));
    }

    /*private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
    private IEnumerator BloodSpray(Vector3 pos)
    {
        Instantiate(_rotEffectParticle, pos, Quaternion.LookRotation(pos));
        //sphere.transform.position = pos;

        yield return new WaitForSeconds(1f);

        
    }*/
}
