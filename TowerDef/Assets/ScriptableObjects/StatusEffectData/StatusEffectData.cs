using System;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData
{
    public abstract class StatusEffectData : ScriptableObject
    {
        [Header("Basic Info")]
        public new string name = "New Effect";
        [HideInInspector]
        public StatusEffect effect = null;
        public ElementType elementType;
        [Tooltip("Particle effect prefab")]
        public ParticleSystem particleEffectPrefab;
        [Tooltip("Sound effect to play when the effect is applied")]
        public AuidoEvent applySoundEffect;
        public abstract void Initialize(LivingEntity entity);
    }

}