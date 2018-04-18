
using EffectData;
using ScriptableObjects.Enums;
using UnityEngine;
public abstract class DamageEffectData : StatusEffectData 
{
        [Header("Damage Settings")]
        public ElementType type;
        public float damage;
        public override void BeginEffect(Enemy e, ParticleSystem particleSystem){}

        public override void EndEffect(Enemy e){}
}