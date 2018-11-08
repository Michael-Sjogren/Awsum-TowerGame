
using Buildings;
using UnityEngine;

public class BeamLauncher : MonoBehaviour ,  ILauncher
{
    public AudioEvent fireAudioEvent;
    public LightningVisualEffect beamVisualEffect;
    public GameObject firePoint;
    public Ability ability;

    private Tower owner;
    private AudioSource source;


    private void Start()
    {
        owner = GetComponent<Tower>();
        source = GetComponent<AudioSource>();
        
    }

    public void Launch(GameObject target)
    {
        var entity = target.GetComponent<LivingEntity>();
        if (entity == null)
        {
            beamVisualEffect.targets[1] = null;
            return;
        }
        if (source != null)
        {
            if (fireAudioEvent != null)
            {
                fireAudioEvent.Play(source);
            }
        }
        if(ability != null)
        {
            ability.TriggerAbility(entity.gameObject, entity.centerPosition.transform.position );
        }
        beamVisualEffect.targets[0] = firePoint;
        beamVisualEffect.targets[1] = entity.centerPosition;
        beamVisualEffect.Play(false);
        float damage = owner.Damage.Value;
        entity.TakeDamage(damage);

    }

	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
