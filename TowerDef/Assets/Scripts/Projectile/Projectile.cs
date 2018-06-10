
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    public ProjectileData projectileData;

    protected LivingEntity target;
    protected float damage;
    protected VisualEffect projectileEffect;

    public virtual void SetTarget( LivingEntity target , float damage)
    {
        this.target = target;
        this.damage = damage;
        Fire();
    }

    public abstract void OnTargetHit();

    public abstract void Fire();

}
