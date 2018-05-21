using Effects;


using System;
using Assets.ScriptableObjects.StatusEffectData;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class Debuff : StatusEffect
    {
        protected ParticleSystem system;
        protected Coroutine routine;
        protected float reapplyCooldown;
        protected Enemy enemy;
        public ElementType elementType;
        public ElementType oppositeType;
        private new DebuffData data;
        protected AuidoEvent applyEffect;
        public Debuff(DebuffData data , Enemy e) : base(data)
        {
            this.data = data;
            this.name = data.name;
            this.enemy = e;
            this.reapplyCooldown = data.reapplyCooldown;
            this.elementType = data.elementType;
            this.applyEffect = data.applySoundEffect;
        }
        public override void BeginEffect()
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
        public override void EndEffect()
        {
            if(system != null)
                system.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            if (routine != null)
                enemy.StopCoroutine(routine);
            RemoveSelf();
        } 

        private void RemoveSelf()
        {
            enemy.RemoveDebuff(this);
        }
    }
}
