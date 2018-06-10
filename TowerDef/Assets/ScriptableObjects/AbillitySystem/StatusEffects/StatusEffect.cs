using System;
using System.Collections;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffects
{
    public abstract class StatusEffect : Ability
    {
        public ElementType elementType;
        public float reapplyCooldown;

        public override void TriggerAbility(GameObject obj , Vector3 center)
        {
            var entity = obj.GetComponent<LivingEntity>();
            var system = obj.GetComponent<StatusEffectSystem>();
            if (entity != null && system != null)
            {
                system.AddStatusEffect(this);
            }
        }

        public override void PlayVisualEffect(VisualEffect effect)
        {
            effect.Play();
        }

        protected virtual void AddVisualEffectAndInstantiate(LivingEntity entity)
        {
            if(abillityVisualEffect != null)
            {
                var system = entity.GetComponent<StatusEffectSystem>();
                var vfx = Instantiate(abillityVisualEffect, entity.centerPosition.transform );
                vfx.transform.localPosition = Vector3.zero;
                system.AddVisualEffect(this, vfx);
                PlayVisualEffect(vfx);
            }
        }

        protected virtual void RemoveVisualEffectAndStop(LivingEntity entity)
        {
             var system = entity.GetComponent<StatusEffectSystem>();
             var vfx = system.GetVisualEffect(this);
             StopVisualEffect(vfx);
             system.RemoveVisualEffect(this);
        }

        public override void StopVisualEffect(VisualEffect effect)
        {
           // var system = entity.GetComponent<StatusEffectSystem>();
           // var vfx = system.GetVisualEffect(this);
            if (effect != null)
            {
                var visualEffect = effect.GetComponent<VisualEffect>();
                visualEffect.StartCoroutine(visualEffect.Stop(0));    
            }
           // system.RemoveVisualEffect(this);
        }

        public abstract void BeginEffect(LivingEntity entity);
        public abstract IEnumerator DoEffect(LivingEntity entity);
        public abstract void EndEffect(LivingEntity entity);

        public virtual void RemoveEffect(LivingEntity entity)
        {
            var system = entity.GetComponent<StatusEffectSystem>();
            if(this.GetType().IsInstanceOfType(typeof(StackableEffect)))
            {
                var stackableEffect = this as StackableEffect;
                system.RemoveStackEffect(stackableEffect);
            }
            system.RemoveStatusEffect(this);
        }
    }

}