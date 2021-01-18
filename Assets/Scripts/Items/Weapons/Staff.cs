using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Staff : Weapon
{
    public GameObject projectilePrefab;
    [SerializeField] private LayerMask enemyLayer;
    private readonly WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private Transform weaponTip;
    private Camera playerCamera;
    private LineRenderer laserLine;
    private GameObject player;
    private float nextFire;

    private void Start()
    {
        weaponTip = GameObject.Find("StaffTip").GetComponent<Transform>();
        playerCamera = GameObject.Find("PlayerHead").GetComponent<Camera>();
        player = GameObject.Find("Player");
        laserLine = GetComponent<LineRenderer>();
        fireRate = 1;
        hitForce = 100f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerCharacter>().currentHealth > 0)
            Shoot();
        //projectilePrefab.GetComponent<Renderer>().material = player.GetComponent<PlayerCharacter>().eye1.GetComponent<Renderer>().material;
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
                    laserLine.SetPosition(1, hit.point);
                    hitObject.HitReaction(hit.point, transform.rotation, weaponDamage);
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    ProjectileLaunch(hit.point);
                }
                else
                {
                    laserLine.SetPosition(1, hit.point);
                    ProjectileLaunch(hit.point);
                    
                }

            }
        }
    }

    private void ProjectileLaunch(Vector3 target)
    {
        GameObject projectile = Instantiate(projectilePrefab, weaponTip.transform.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = (target - playerCamera.transform.position).normalized * 22f;
        projectileRb.rotation = Quaternion.LookRotation(projectileRb.velocity);
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

}
