

using ScriptableObjects.Enums;
using UnityEngine;

namespace EffectData {
    [CreateAssetMenu( fileName="New Dot" ,  menuName="StatusEffects/Debuffs/New Dot")]
    public class DamageOverTimeData : TimedEffectData
    {
        [Header("Damage Settings")]
        public ElementType type;
        public float totalDamage;

        public override void BeginEffect( Enemy e , ParticleSystem particleSystem )
        {
            base.BeginEffect(e , particleSystem );
        }

        public override void DoEffect( Enemy e)
        {
            e.TakeDamage( totalDamage / intervalAmount );
        }

        public override void EndEffect( Enemy e)
        {
            base.EndEffect(e);
        }

        public override void UpdateEffect( Enemy e , float deltaTime )
        {
            
        }
    }

}