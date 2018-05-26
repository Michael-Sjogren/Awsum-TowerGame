using Effects;


using System;
using Assets.ScriptableObjects.StatusEffectData;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public abstract class Debuff : StatusEffect
    {
        protected float reapplyCooldown;

        public Debuff(DebuffData data , LivingEntity entity) : base( data , entity )
        {
            reapplyCooldown = data.reapplyCooldown;
        }
    } 
}
