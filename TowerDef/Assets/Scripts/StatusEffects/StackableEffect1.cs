using Effects;
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffectData;
/*
public class StackableEffect1 : StatusEffect , ITimedEffect
{
    private Stack<StatModifer> stacks;

    private StatusEffectData maxStacksEffect;

    private int maxStacks;

    private StatModifer modifer;
    private AttributeEnum attrributeType;

    public Stopwatch Timer { get; set; }

    public float EffectLifeTime { get; set;}

    public float ElapsedTime { get; set; }

    public StackableEffect1(StackableEffectData data, LivingEntity e) : base(data, e)
    {
        maxStacks = data.maxStacks;
        stacks = new Stack<StatModifer>(maxStacks);
        modifer = data.modifer;
        attrributeType =  data.attriEnum;
        maxStacksEffect = data.maxStackEffect;
        Timer = new Stopwatch();
    }


    public void AddToStack()
    {
        if(stacks.Count < maxStacks) 
        {
            var newModifer = new StatModifer(modifer.Value , modifer.Type , modifer.Order , modifer.Source );
            entity.AddStatModifer( newModifer, attrributeType);
            stacks.Push(newModifer);
            UnityEngine.Debug.Log(stacks.Count);
        }
        else if(stacks.Count >= maxStacks)
        {
            StatusEffectData effect = maxStacksEffect;
            entity.GetComponent<StatusEffectSystem>().AddStatusEffect(effect);
            EndEffect();
        } 
    }

    public override void BeginEffect()
    {
        AddToStack();
        coroutine = entity.StartCoroutine(DoEffectOverTime());
    }

    public void UpdateEffectLifeTime(float deltaTIme)
    {
        if (stacks.Count >= maxStacks)
        {
            StatusEffectData effect = maxStacksEffect;
            entity.GetComponent<StatusEffectSystem>().AddStatusEffect(effect);
            EndEffect();
        }
    }

    public override void EndEffect()
    {
        StopParticleEffect();
        base.EndEffect();
    }

    public IEnumerator DoEffectOverTime()
    {
        while (EffectLifeTime >= ElapsedTime)
        {
            UpdateEffectLifeTime(Time.deltaTime);
            yield return null;
        }
        Timer.Reset();
        Timer.Stop();

        if (stacks.Count > 0)
        {
            StatModifer modifer = stacks.Pop();
            entity.RemoveStatModifer(modifer, attrributeType);
            float value = entity.GetStat(attrributeType).Value;
            entity.StopCoroutine(coroutine);
            coroutine = entity.StartCoroutine(DoEffectOverTime());
        }
        else
        {
            EndEffect();
        }

        yield return null;
    }
}
*/