using Assets.ScriptableObjects.StatusEffectData;
using UnityEngine;
[CreateAssetMenu(menuName ="Abilities/AreaOfEffect/Explostion")]
public class ExplostionAbilitytData : AbilityData
{
    public float effectRadius;
    public float damage;
    public float hitForce;
    public string targetTag;

    public GameObject explostionEffect;
    private Vector3 pos;

    public override void Initialize(GameObject obj)
    {
        pos = obj.transform.position;
        TriggerAbillity();
    }

    public override void TriggerAbillity()
    {
        var colliders = Physics.OverlapSphere(pos, effectRadius);

        Instantiate(explostionEffect, pos , Quaternion.identity );

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
    }


}