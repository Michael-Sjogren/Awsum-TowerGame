
using System;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
public class DamageOverTimeEffect : TimedEffect
{
    private float damage;
    public DamageOverTimeEffect(DamageOverTimeData data , Enemy e) : base(data, e)
    {
       damage = data.totalDamage;
    }

    public override void BeginEffect()
    {
        base.BeginEffect();
    }

    public override void DoEffect()
    {
       float value = damage / (effectLifeTime / timeTickGap);
       enemy.TakeDamage(value);
    }

    public override void EndEffect()
    {
        system.Stop(true);
        base.EndEffect();
    }
}