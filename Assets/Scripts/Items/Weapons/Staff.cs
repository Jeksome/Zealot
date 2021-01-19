using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private LayerMask enemyLayer;
    private readonly WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private Transform weaponTip;
    private Camera playerCamera;
    private GameObject player;
    private float nextFire;

    private void Start()
    {
        weaponTip = GameObject.Find("StaffTip").GetComponent<Transform>();
        playerCamera = GameObject.Find("PlayerHead").GetComponent<Camera>();
        player = GameObject.Find("Player");
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
            int weaponDamage = Random.Range(1, 3);
            player.GetComponent<PlayerCharacter>().Hurt(weaponDamage);
            
            if (Physics.Raycast(ray, out hit, enemyLayer))
            {
                nextFire = Time.time + fireRate;
                ProjectileLaunch(hit.point);
            }
        }
    }

    private void ProjectileLaunch(Vector3 target)
    {
        GameObject projectile = Instantiate(projectilePrefab, weaponTip.transform.position, Quaternion.identity);
        Vector3 projectileVelocity = (target - playerCamera.transform.position).normalized * 20f;
        projectile.GetComponent<Projectile>().GetVelocity(projectileVelocity);
    }
}
