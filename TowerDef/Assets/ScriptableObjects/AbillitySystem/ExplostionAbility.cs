
using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName ="Abilities/AreaOfEffect/Explostion")]
public class ExplostionAbility : Ability
{
    public float effectRadius;
    public float damage;
    public float hitForce;
    public string targetTag;
    private Vector3 pos;



    public override void TriggerAbility(GameObject obj , Vector3 center )
    {
        var pos = center;

        var colliders = Physics.OverlapSphere(pos, effectRadius);

        var vfx = Instantiate( abillityVisualEffect , pos , Quaternion.identity );
        PlayVisualEffect(vfx);
        for (int i = 0; i < colliders.Length; i++)
        {
            var nearbyObj = colliders[i];
            var rb = nearbyObj.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(hitForce, pos, effectRadius);
            }

            var entity = nearbyObj.GetComponent<LivingEntity>();
            if (entity != null && nearbyObj.CompareTag(targetTag))
            {
                entity.TakeDamage(damage);
            }
        }
        StopVisualEffect(vfx);
    }

    public override void StopVisualEffect(VisualEffect vfx)
    {
        vfx.StartCoroutine(vfx.Stop(vfx.lifeTime));
    }

    public override void PlayVisualEffect(VisualEffect effect)
    {
        effect.Play();
    }
}