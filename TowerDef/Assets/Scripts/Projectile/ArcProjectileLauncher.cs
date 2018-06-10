using Buildings;
using UnityEngine;

public class ArcProjectileLauncher : MonoBehaviour, ILauncher
{
    public AudioEvent fireAudioEvent;
    public GameObject projectilePrefab;
    public GameObject firePoint;

    private Tower owner;
    private AudioSource source;

    public float h = 25;

    // Use this for initialization
    void Start()
    {
        owner = GetComponent<Tower>();
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void Launch(GameObject target)
    {
        var entity = target.GetComponent<LivingEntity>();

        var obj = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity).gameObject;
    
        var projectile = obj.GetComponent<SplashProjectile>();
        float gravity = projectile.GetComponent<Gravity>().gravityScale * Physics.gravity.y;
        projectile.velocity = CalculateLaunchVelocity(target , gravity);
        projectile.SetTarget(entity, owner.Damage.Value);
       
    }

    Vector3 CalculateLaunchVelocity(GameObject target , float gravity)
    {
        Vector3 vel = Vector3.zero;
        var tPos = target.transform.position;
        float displacementY = target.transform.position.y - firePoint.transform.position.y;
        Vector3 displacementXZ = new Vector3(tPos.x - firePoint.transform.position.x, 0, tPos.z - firePoint.transform.position.z);

        Vector3 velY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velXZ = displacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity));
        vel = velXZ + velY;
        return vel;
    }
}
