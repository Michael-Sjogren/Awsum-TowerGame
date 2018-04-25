
using System;
using System.Collections;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;
using UnityEngine.AI;


public class TimedAttributeEffect : StatusEffect
{
    protected float Value;
    protected AttributeEnum attriEnum;
    protected float oldValue;
    protected float duration;
    protected bool isPersistent;
    protected float cooldown;
    public TimedAttributeEffect(TimedAttributeEffectData data, Enemy e) : base(data, e)
    {
        attriEnum = data.attributeEnum;
        Value = data.Amount;
        isPersistent = data.isPersistent;
        duration = data.duration;
        cooldown = data.reapplyCooldown;
    }

    public override void BeginEffect()
    {
        base.BeginEffect();
        routine = enemy.StartCoroutine(ApplyAttributeEffect());
    }
    
    public IEnumerator ApplyAttributeEffect()
    {
        switch (attriEnum)
        {
            case AttributeEnum.MovementSpeed:
                if (this.data.name == "Frozen") {
                    oldValue = enemy.MovementSpeed;
                    enemy.MovementSpeed = 0;
                }
                break;
            default: break;
        }
        enemy.UpdateAttributes();
        if (isPersistent)
            yield return null;

        yield return new WaitForSeconds(duration);
        switch (attriEnum)
        {
            case AttributeEnum.MovementSpeed:
                if (this.data.name == "Frozen") 
                {
                    enemy.MovementSpeed = oldValue;
                }
                break;
            default: break;
        }
        system.Stop(true);

        Debug.Log("ReapplyCooldown for " + this.name + ".. " + cooldown);
        enemy.RegisterCooldown( this.name , new Cooldown(reapplyCooldown , this.data.name , enemy ) );
        EndEffect();
        yield return null;
    }

    public override void EndEffect()
    {
        enemy.UpdateAttributes();
        base.EndEffect();
    }
}