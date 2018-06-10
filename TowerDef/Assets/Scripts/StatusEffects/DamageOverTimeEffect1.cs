
using System;
using System.Collections;
using System.Diagnostics;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;
/*
public class DamageOverTimeEffect1 : StatusEffect , ITimedEffect
{
    
    private float damage;
    private int ticks;
    private float tickTimeGap;

    public float EffectLifeTime {get;set;}

    public Stopwatch Timer {get;set;}

    public DamageOverTimeEffect1(DamageOverTimeData data , LivingEntity  e) : base(data, e)
    {
        damage = data.totalDamage;
        ticks = data.tickAmount;
        EffectLifeTime = data.effectLifeTime;
        tickTimeGap = EffectLifeTime / ticks;
        Timer = new Stopwatch();
    }


    public override void BeginEffect()
    {
        PlayParticleEffect();
        coroutine = entity.StartCoroutine(DoEffectOverTime());
    }

    public void DoDamage()
    {
        float value = (damage / ticks );
        entity.TakeDamage(value);
    }

    public override void EndEffect()
    {
        StopParticleEffect();
        base.EndEffect();
    }


    public IEnumerator DoEffectOverTime()
    {
        Timer.Start();
        while (Timer.Elapsed.Seconds <= EffectLifeTime )
        {
            DoDamage();
            yield return new WaitForSeconds(tickTimeGap);
        }
        EndEffect();
        yield return null;
    }
}
*/
