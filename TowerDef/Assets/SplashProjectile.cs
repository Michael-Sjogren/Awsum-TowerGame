

using Assets.ScriptableObjects.Projectile;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SplashProjectile : Projectile
{
    private float splashRadius;
    [HideInInspector]
    public Vector3 velocity;
    public LayerMask targetLayermask;

    public override void Fire()
    {
        var data = projectileData as SplashProjectileData;
        
        if (projectileData.projectileEffect != null)
        {
            projectileEffect = (Instantiate(projectileData.projectileEffect));
            projectileEffect.transform.SetParent(transform);
            projectileEffect.transform.localPosition = Vector3.zero;
            projectileEffect.Play();

        }
        splashRadius = data.splashRadius;
        GetComponent<Rigidbody>().velocity = velocity;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Tower"))
        {
            OnTargetHit();
            Destroy(gameObject);
        }
    }

    public override void OnTargetHit()
    {
        var colliders = Physics.OverlapSphere(transform.position, splashRadius, targetLayermask);
        // do the hit vfx before dmg
        var vfx = Instantiate(projectileData.projectileHitEffect , transform.position , projectileData.projectileHitEffect.transform.rotation );
        
        for (int i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i];
            var entity = collider.GetComponent<LivingEntity>();
            if (entity != null)
            {
                entity.TakeDamage(damage);
            }
        }

        if (projectileData.hitSound != null)
        {
            projectileData.hitSound.Play(target.GetComponent<AudioSource>());
        }

        StartCoroutine(vfx.Stop(vfx.lifeTime));
    }
}

