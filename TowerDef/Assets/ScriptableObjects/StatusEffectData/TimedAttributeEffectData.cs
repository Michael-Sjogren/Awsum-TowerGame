using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData
{
    [CreateAssetMenu(menuName="StatusEffects/Timed Effects/New Attribute effect")]
    public class TimedAttributeEffectData : StatusEffectData 
    {
        public AttributeEnum attributeEnum;
        public float Amount;
        [Tooltip("The lifetime of the effect , only works if the persitent variable is false")]
        public float duration;
        [Tooltip("If ticked true , this effect will last as long as the enemy is alive")]
        public bool isPersistent;

        public override void Initialize(Enemy e)
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
        MagicArmor
}