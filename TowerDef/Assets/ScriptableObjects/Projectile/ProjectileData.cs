
using UnityEngine;

[CreateAssetMenu(menuName="Projectiles/New Simple Projectile" , fileName="New Simple Projectile")]
public abstract class ProjectileData : ScriptableObject
{
    public VisualEffect projectileEffect;
    public VisualEffect projectileHitEffect;

    public AudioEvent hitSound;

    public Ability ability;
}