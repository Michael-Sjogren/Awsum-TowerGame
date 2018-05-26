using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData {
    [CreateAssetMenu(fileName = "New Dot", menuName = "StatusEffects/Debuffs/New Dot")]
    public class DamageOverTimeData : TimedEffectData
    {
        [Header("Damage Settings")] 
        public float totalDamage;

        public override void Initialize(LivingEntity e)
        {
            effect = new DamageOverTimeEffect( this , e );
        }
    }
}