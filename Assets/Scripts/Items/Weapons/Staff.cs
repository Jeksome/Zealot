using UnityEngine;

public class Staff : Weapon
{
    private Transform weaponTip;
    private Camera playerCamera;
    private PlayerCharacter player;
    private Animator staffAnimator;
    private float nextFire;

    private void Start()
    {
        staffAnimator = GetComponent<Animator>();
        weaponTip = GameObject.Find("StaffTip").GetComponent<Transform>();
        playerCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        fireRate = 1.2f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.IsAlive)
            Shoot();
    }

    public override void Shoot()
    {
        Vector3 rayOrigin = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(rayOrigin);

        if (Time.time > nextFire && Time.timeScale != 0)
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
        GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject("Missile");
        float velocityMultiplier = 20f;
        if (projectile != null)
        {
            projectile.transform.position = weaponTip.transform.position;
            projectile.SetActive(true);
        }
        Vector3 projectileVelocity = (target - playerCamera.transform.position).normalized * velocityMultiplier;
        projectile.GetComponent<Projectile>().ProjectileVelocity = projectileVelocity;
    }
}
