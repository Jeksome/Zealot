using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 ProjectileVelocity { set { projectileVelocity = value; } }
    private Vector3 projectileVelocity;

    public int ProjectileDamage { set { projectileDamage = value; } }
    private int projectileDamage; 

    private Rigidbody projectileRigidBody;
    private AudioSource projectileAudioSource;

    #pragma warning disable 0649
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject smallHitEffect;
    [SerializeField] AudioClip projectileSound;
    #pragma warning restore 0649

    private void OnEnable()
    {
        projectileRigidBody = GetComponent<Rigidbody>();
        projectileAudioSource = GetComponent<AudioSource>();
        projectileAudioSource.PlayOneShot(projectileSound);
    } 

    private void FixedUpdate() => projectileRigidBody.velocity = projectileVelocity;

    private void OnCollisionEnter(Collision target)
    {       
        HitDetector hitObject = target.transform.gameObject.GetComponent<HitDetector>();
        GameObject onHitblast = ObjectPooler.Instance.GetPooledObject("ExplosionBig");
        GameObject onMissblast = ObjectPooler.Instance.GetPooledObject("ExplosionSmall");        

        if (hitObject)
        {
            Blast(onHitblast);
            hitObject.HitReaction(transform.position, transform.rotation, target.gameObject, projectileDamage);
        }
        else
        {
            Blast(onMissblast);
        }
        gameObject.SetActive(false);
    }

    private void Blast (GameObject blastEffect)
    {
        if (blastEffect != null)
        {
            blastEffect.transform.position = transform.position;
            blastEffect.SetActive(true);            
        }
    }
}
