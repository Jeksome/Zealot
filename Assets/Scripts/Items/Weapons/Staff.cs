using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Staff : Weapon
{
    public Transform weaponTip;
    public TMP_Text ammoValue;
    public Camera playerCamera;
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;
    private GameObject player;

    public LayerMask enemyLayer;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
        fireRate = 1;
        hitForce = 100f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerCharacter>().currentHealth > 0)
            Shoot();
    }

    public override void Shoot()
    {
        Vector3 rayOrigin = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(rayOrigin);

        if (Time.time > nextFire)
        {
            StartCoroutine(ShotEffect());
            laserLine.SetPosition(0, weaponTip.position);
            weaponDamage = Random.Range(1, 3);
            player.GetComponent<PlayerCharacter>().Hurt(weaponDamage);
            
            if (Physics.Raycast(ray, out hit, enemyLayer))
            {
                nextFire = Time.time + fireRate;
                HitDetector hitObject = hit.transform.gameObject.GetComponent<HitDetector>();               
                
                if (hitObject != null)
                {
                    ProjectileLaunch();
                    laserLine.SetPosition(1, hit.point);
                    hitObject.HitReaction(hit.point, transform.rotation, weaponDamage);
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
                else
                {
                    laserLine.SetPosition(1, hit.point);
                    ProjectileLaunch();
                }
            }
        }
    }


    private void ProjectileLaunch()
    {
        //TODO missile
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

}
