using Buildings;
using UnityEngine;

public class HomingProjectileLauncher : MonoBehaviour , ILauncher
{
    public AudioEvent fireAudioEvent;
    public GameObject projectilePrefab;
    public GameObject firePoint;

    private Tower owner;
    private AudioSource source;

    void Start()
    {
        owner = GetComponent<Tower>();
    }

    public void Launch( GameObject target )
    {
        var entity = target.GetComponent<LivingEntity>();
        if(entity == null)
        {
            return;
        }
        source = GetComponent<AudioSource>();
        
        if(source != null) 
        {
            if(fireAudioEvent != null) 
            {
			    fireAudioEvent.Play(source);
            }
        }

        var p = Instantiate ( projectilePrefab , firePoint.transform.position , firePoint.transform.rotation ).gameObject;
        Projectile projectile = p.GetComponent<Projectile>();
        float damage = owner.Damage.Value;
        projectile.SetTarget( entity , damage );
    }
}