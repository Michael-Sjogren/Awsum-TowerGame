using System;
using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffects
{
    [CreateAssetMenu(menuName="StatusEffects/Timed Effects/New Attribute effect")]
    public class AttributeEffect : TimedEffect
    {
        public AttributeEnum attributeEnum;
        public float Amount;
        public StatModType type;
        [Tooltip("If ticked true , this effect will last as long as the enemy is alive")]
        public bool isPersistent;

        public override void BeginEffect(LivingEntity entity)
        {
            AddVisualEffectAndInstantiate(entity);
            entity.StartCoroutine(DoEffect(entity));
        }

        public override IEnumerator DoEffect(LivingEntity entity)
        {

            var modifer = new StatModifer(Amount, type, this);
            entity.AddStatModifer(modifer, attributeEnum);
            if(!isPersistent)
            {
                yield return new WaitForSeconds(effectLifeTime);
                entity.RemoveStatModifer(modifer, attributeEnum);
                entity.GetComponent<StatusEffectSystem>().RegisterDebuffCooldown(new Cooldown(reapplyCooldown, name));
                EndEffect(entity);
                yield return null;
            }
        }

        public override void EndEffect(LivingEntity entity)
        {
            RemoveVisualEffectAndStop(entity);
            RemoveEffect(entity);
        }
    }
}
[Serializable]
public enum AttributeEnum
{
        Health,
        MovementSpeed,
        Armor,
        LightningResistance,
        EarthResitance,
        FrostResistance,
        WaterResistance,
        FireResistance
}