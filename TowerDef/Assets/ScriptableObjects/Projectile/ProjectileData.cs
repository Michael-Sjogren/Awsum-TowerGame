using Assets.ScriptableObjects.StatusEffectData;
using UnityEngine;

[CreateAssetMenu(menuName="Projectiles/New Projectile" , fileName="New Projectile")]
public class ProjectileData : ScriptableObject {
    public float speed;
    public ParticleSystem projectileEffect;
    public ParticleSystem projectileHitEffect;
    public DebuffData debuffData;
}