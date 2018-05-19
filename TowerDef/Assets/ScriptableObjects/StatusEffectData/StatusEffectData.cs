using System;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData {
    public abstract class StatusEffectData : ScriptableObject
    {
        [Header("Basic Info")]
        public new string name = "New Effect";

        [Tooltip("The time before the next status effect of the same type can be applied")]
        public float reapplyCooldown;
        
        [Tooltip("This can be unassigned , it will count as nothing")]
        public ElementType elementType;
        public ElementType oppositeType;

        [Tooltip("Particle effect prefab")]
        public ParticleSystem particleEffectPrefab;
        [Tooltip("Sound effect to play when the effect is applied")]
        public AuidoEvent applySoundEffect;

        [HideInInspector]
        public StatusEffect effect = null;

        public abstract void Initialize(Enemy e);
    }

}