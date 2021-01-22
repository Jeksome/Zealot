using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    private Transform weaponTip;
    private Camera playerCamera;
    private GameObject player;
    private Animator staffAnimator;
    private float nextFire;

    private void Start()
    {
        staffAnimator = GetComponent<Animator>();
        weaponTip = GameObject.Find("StaffTip").GetComponent<Transform>();
        playerCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        fireRate = 1.2f;
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
            
            if (Physics.Raycast(ray, out hit))
            {
                nextFire = Time.time + fireRate;
                ProjectileLaunch(hit.point);
                staffAnimator.SetTrigger("Shoot");
            }
        }
    }

    private void ProjectileLaunch(Vector3 target)
    {
        GameObject project = ObjectPooler.SharedInstance.GetPooledObject("Missile");
        if (project != null)
        {
            project.transform.position = weaponTip.transform.position;
            project.transform.rotation = Quaternion.identity;
            project.SetActive(true);
        }
        Vector3 projectileVelocity = (target - playerCamera.transform.position).normalized * 20f;
        project.GetComponent<Projectile>().GetVelocity(projectileVelocity);
    }
}
