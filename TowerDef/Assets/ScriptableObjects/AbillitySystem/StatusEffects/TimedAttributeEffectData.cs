using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData
{
    [CreateAssetMenu(menuName="StatusEffects/Timed Effects/New Attribute effect")]
    public class TimedAttributeEffectData : DebuffData 
    {
        public AttributeEnum attributeEnum;
        public float Amount;
        [Tooltip("The lifetime of the effect , only works if the persitent variable is false")]
        public float duration;
        public StatModType type;
        [Tooltip("If ticked true , this effect will last as long as the enemy is alive")]
        public bool isPersistent;
        public override void Initialize(LivingEntity e)
        {
            effect = new TimedAttributeEffect(this , e);
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