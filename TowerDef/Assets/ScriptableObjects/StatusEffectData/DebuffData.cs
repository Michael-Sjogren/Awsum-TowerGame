using Assets.ScriptableObjects.StatusEffectData;
using UnityEngine;

public abstract class DebuffData : StatusEffectData
{
    [Header("Debuff Data")]
    [Tooltip("The time before the next status effect of the same type can be applied")]
    public float reapplyCooldown;
}