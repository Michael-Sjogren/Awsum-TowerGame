using Buildings;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour , ILauncher
{
    public AuidoEvent fireAudioEvent;
    public GameObject projectilePrefab;
    public GameObject firePoint;
    private Stat Damage;
    private Tower owner;
    private AudioSource source;

    void Start()
    {
        owner = GetComponent<Tower>();
    }

    public void Launch( GameObject target )
    {
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
        projectile.SetTarget(target , damage );
    }
}