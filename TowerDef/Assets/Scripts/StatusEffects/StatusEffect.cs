
using System;
using Assets.ScriptableObjects.StatusEffectData;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public abstract class StatusEffect
    {
        public string name;
        public ElementType elementType;

        protected readonly StatusEffectData data;

        protected LivingEntity entity;

        protected ParticleSystem particleSystem;
        protected Coroutine coroutine;

        public StatusEffect(StatusEffectData data , LivingEntity entity)
        {
            this.data = data;
            name = data.name;
            elementType = data.elementType;
            this.entity = entity;
        }

        public abstract void BeginEffect();

        public virtual void EndEffect()
        {
            RemoveSelf();
        }

        public void PlayParticleEffect()
        {
            if(data.particleEffectPrefab != null)
            {
                particleSystem = UnityEngine.Object.Instantiate(data.particleEffectPrefab , entity.transform , false );
                particleSystem.transform.localPosition = Vector3.zero;
                particleSystem.Play(true);
            }
        }

        public void StopParticleEffect(ParticleSystemStopBehavior stopBehaviour = ParticleSystemStopBehavior.StopEmitting )
        {
            if (particleSystem != null)
            {
                particleSystem.Stop(true , stopBehaviour);
            }
        }

        private void RemoveSelf()
        {
            entity.RemoveStatusEffect(this);
        }
    }
}

