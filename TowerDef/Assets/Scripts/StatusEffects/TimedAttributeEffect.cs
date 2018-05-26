
using System;
using System.Collections;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;
using UnityEngine.AI;


public class TimedAttributeEffect : Debuff
{
    protected float Value;
    protected AttributeEnum attriEnum;
    protected float oldValue;
    protected float duration;
    protected bool isPersistent;
    protected float cooldown;
    protected StatModType type;
    private Coroutine routine;

    public TimedAttributeEffect(TimedAttributeEffectData data, LivingEntity e) : base(data, e)
    {
        attriEnum = data.attributeEnum;
        Value = data.Amount;
        isPersistent = data.isPersistent;
        duration = data.duration;
        cooldown = data.reapplyCooldown;
        type = data.type;
        entity = e;
    }

    public override void BeginEffect()
    {
        routine = entity.StartCoroutine(ApplyAttributeEffect());
    }
    
    public IEnumerator ApplyAttributeEffect()
    {
        var modifer = new StatModifer(Value , type ,this);

        entity.AddStatModifer(modifer , attriEnum);
        yield return new WaitForSeconds(duration);
        entity.RemoveStatModifer(modifer , attriEnum);
        StopParticleEffect(ParticleSystemStopBehavior.StopEmitting);
        entity.RegisterDebuffCooldown( new Cooldown(reapplyCooldown , data.name) );
        EndEffect();
        yield return null;
    }

    public override void EndEffect()
    {
        base.EndEffect();
    }
}