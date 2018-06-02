using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData {
    [CreateAssetMenu(fileName = "New Dot", menuName = "StatusEffects/Debuffs/New Dot")]
    public class DamageOverTimeData : TimedEffectData
    {
        [Header("Damage Settings")] 
        public float totalDamage;

        [Tooltip("The amount of times the effect will be invoked during its lifetime \n, if zero the effect will not call 'DoEffect'" +
        " \n, if it is 1 it will call DoEffect once and then destroy itself when the lifetime is over")]
        [Range(1, 500)]
        public int tickAmount;

        public override void Initialize(LivingEntity e)
        {
            effect = new DamageOverTimeEffect( this , e );
        }
    }
}