using System;
using System.Collections;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffects {
    [CreateAssetMenu(fileName = "New Dot", menuName = "StatusEffects/Debuffs/New Dot")]
    public class DamageOverTime : TimedEffect
    {
        [Header("Damage Settings")] 
        public float totalDamage;

        [Tooltip("The amount of times the effect will be invoked during its lifetime \n, if zero the effect will not call 'DoEffect'" +
        " \n, if it is 1 it will call DoEffect once and then destroy itself when the lifetime is over")]
        [Range(1, 500)]
        public int tickAmount;

        public override void BeginEffect(LivingEntity entity)
        {
            AddVisualEffectAndInstantiate(entity);
            entity.StartCoroutine(DoEffect(entity));
        }

        public override IEnumerator DoEffect( LivingEntity entity )
        {
            int currentTicks = 0;
            while (currentTicks < tickAmount)
            {
                yield return new WaitForSeconds(effectLifeTime / tickAmount );
                currentTicks++;
                entity.TakeDamage(totalDamage / tickAmount);
            }
            EndEffect(entity);
        }

        public override void EndEffect(LivingEntity entity)
        {
            RemoveVisualEffectAndStop(entity);
            RemoveEffect(entity);
        }
    }
}