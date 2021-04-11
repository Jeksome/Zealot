using UnityEngine;

public class Staff : Weapon
{
    #pragma warning disable 0649
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerCharacter player;
    #pragma warning restore 0649

    private Animator staffAnimator;
    private float nextFire;

    private void Start()
    {
        staffAnimator = GetComponent<Animator>();
        fireRate = 1.2f;
        weaponDamage = 2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.CanCast)
            Shoot();
    }

    public override void Shoot()
    {
        Vector3 rayOrigin = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(rayOrigin);

        if (Time.time > nextFire && Time.timeScale != 0)
        {
            player.GetHurt();         
            
            if (Physics.Raycast(ray, out hit))
            {
                nextFire = Time.time + fireRate;
                ProjectileLaunch(hit.point);
                staffAnimator.SetTrigger("Shoot");
            }
        }
    }

    public override void ProjectileLaunch(Vector3 target)    
    {
        GameObject projectile = ObjectPooler.Instance.GetPooledObject("Missile");
        if (projectile != null)
        {
            float velocityMultiplier = 20f;
            Vector3 projectileVelocity = (target - playerCamera.transform.position).normalized * velocityMultiplier;
            projectile.transform.position = weaponTip.transform.position;
            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().ProjectileVelocity = projectileVelocity;
            projectile.GetComponent<Projectile>().ProjectileDamage = weaponDamage;        
        }        
    }
}
