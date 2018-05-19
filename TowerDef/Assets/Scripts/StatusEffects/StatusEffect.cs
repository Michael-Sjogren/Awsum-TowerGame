
using System;
using Assets.ScriptableObjects.StatusEffectData;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class StatusEffect
    {
        public string name;
        protected ParticleSystem system;
        protected Coroutine routine;
        protected float reapplyCooldown;
        protected Enemy enemy;
        public ElementType elementType;
        public ElementType oppositeType;
        public readonly StatusEffectData data;
        protected AuidoEvent applyEffect;
        public StatusEffect(StatusEffectData data, Enemy e)
        {
            this.data = data;
            this.name = data.name;
            this.enemy = e;
            this.reapplyCooldown = data.reapplyCooldown;
            this.elementType = data.elementType;
            this.oppositeType = data.oppositeType;
            this.applyEffect = data.applySoundEffect;
        }
        public virtual void BeginEffect()
        {
            this.system = GameObject.Instantiate(data.particleEffectPrefab , enemy.transform);
            system.transform.localPosition = Vector3.zero;
            this.system.Play(true);
            if(enemy.GetComponent<AudioSource>() != null) 
            {
                if(applyEffect != null)
                    applyEffect.Play(enemy.GetComponent<AudioSource>());
            }
            else 
            {

            }
        }
        public virtual void EndEffect()
        {
            if(system != null)
                system.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            if (routine != null)
                enemy.StopCoroutine(routine);
            RemoveSelf();
        } 

        private void RemoveSelf()
        {
            enemy.RemoveEffect(this);
        }
    }
}
