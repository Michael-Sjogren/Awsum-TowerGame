
using Assets.ScriptableObjects.StatusEffectData;
using ScriptableObjects.Enums;
using UnityEngine;

public abstract class DebuffData : StatusEffectData
{
    [Tooltip("The time before the next status effect of the same type can be applied")]
    public float reapplyCooldown;

    [Tooltip("This can be unassigned , it will count as nothing")]
    public ElementType elementType;

    [Tooltip("Particle effect prefab")]
    public ParticleSystem particleEffectPrefab;
    [Tooltip("Sound effect to play when the effect is applied")]
    public AuidoEvent applySoundEffect;
    public abstract void Initialize(Enemy e);
}