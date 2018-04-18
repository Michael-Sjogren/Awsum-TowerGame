using EffectData;
using UnityEngine;

[CreateAssetMenu(menuName="Projectiles/New Projectile" , fileName="New Projectile")]
public class ProjectileData : ScriptableObject {

    public float damage;
    public float speed;
    public ParticleSystem projectileEffect;
    public StatusEffectData effectData;
}