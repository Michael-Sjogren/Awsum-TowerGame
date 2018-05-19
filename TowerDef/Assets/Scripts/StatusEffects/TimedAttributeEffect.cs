
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
    protected StatModType type;
    public TimedAttributeEffect(TimedAttributeEffectData data, Enemy e) : base(data, e)
    {
        attriEnum = data.attributeEnum;
        Value = data.Amount;
        isPersistent = data.isPersistent;
        duration = data.duration;
        cooldown = data.reapplyCooldown;
        type = data.type;
    }

    public override void BeginEffect()
    {
        base.BeginEffect();
        routine = enemy.StartCoroutine(ApplyAttributeEffect());
    }
    
    public IEnumerator ApplyAttributeEffect()
    {
        var modifer = new StatModifer(Value , type ,this);
        switch(attriEnum) 
        {
            case AttributeEnum.MovementSpeed:
                enemy.MovementSpeed.AddModifer(modifer);
            break;
            case AttributeEnum.MagicArmor:
                //enemy.MovementSpeed.AddModifer(modifer);
            break;
            case AttributeEnum.Armor:

            break;
            case AttributeEnum.Health:
             
            break;
            default: break;
        }
        enemy.UpdateStats();
        yield return new WaitForSeconds(duration);
        switch(attriEnum) 
        {
            case AttributeEnum.MovementSpeed:
                enemy.MovementSpeed.RemoveModifer(modifer);
            break;
            case AttributeEnum.MagicArmor:
                //enemy.MovementSpeed.AddModifer(modifer);
            break;
            case AttributeEnum.Armor:

            break;
            case AttributeEnum.Health:
                
            break;
        }
        enemy.UpdateStats();

        system.Stop(true);

        Debug.Log("ReapplyCooldown for " + this.name + ".. " + cooldown);
        enemy.RegisterCooldown( new Cooldown(reapplyCooldown , this.data.name , enemy ) );
        EndEffect();
        yield return null;
    }

    public override void EndEffect()
    {
        base.EndEffect();
    }
}