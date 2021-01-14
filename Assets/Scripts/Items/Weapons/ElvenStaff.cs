using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElvenStaff : Weapon
{
    public Transform weaponTip;
    public TMP_Text ammoValue;
    public Camera playerCamera;
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;

    private void Start()
    {
        //playerCamera = GetComponent<Camera>();
        laserLine = GetComponent<LineRenderer>();

        ammo = 0;
        maxAmmo = 50;
        fireRate = 1;
        hitForce = 100f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Shoot();
        }

        if (ammo > maxAmmo)
        {
            ammo = 50;
            Debug.Log("Maximum ammo");
        }
        else if (ammo <= 0)
        {
            ammo = 0;
        }

        if (ammo > 0)
        {
            ammoValue.text = "Crystals:  " + ammo.ToString();
        }
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
            ammo -= 1;
            weaponDamage = Random.Range(1, 3);
            Debug.Log($"your ammo is {ammo} and damage is {weaponDamage}");
            
            if (Physics.Raycast(ray, out hit))
            {
                nextFire = Time.time + fireRate;
                GameObject hitObject = hit.transform.gameObject;
                HitDetector target = hitObject.GetComponent<HitDetector>();
                laserLine.SetPosition(1, hit.point);
                if (target != null)
                {
                    ProjectileLaunch();
                    target.HitReaction(hit.point, transform.rotation, weaponDamage);
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
                else
                {
                    laserLine.SetPosition(1, hit.point);
                    ProjectileLaunch();
                    //TODO StartCoroutine(walldamage(hit.point));
                }
            }
        }
    }

    public void IncreaseAmmo(int addAmmo)
    {
        ammo += addAmmo;
        Debug.Log($"Your ammo increased by {addAmmo} and is {ammo} now");
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
