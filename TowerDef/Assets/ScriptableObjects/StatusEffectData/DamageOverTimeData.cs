using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData {
    [CreateAssetMenu(fileName = "New Dot", menuName = "StatusEffects/Debuffs/New Dot")]
    public class DamageOverTimeData : TimedEffectData
    {
        [Header("Damage Settings")] 
        public ElementType type;
        public float totalDamage;

        public override void Initialize(Enemy e)
        {
            effect = new DamageOverTimeEffect( this , e );
        }
    }
}