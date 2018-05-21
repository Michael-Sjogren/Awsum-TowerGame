using Effects;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffectData;

public class StackableDebuff : TimedEffect
{
    public Stack<StatModifer> stacks;
    public StatusEffectData maxStacksEffect;
    public int maxStacks;
    private StatModifer modifer;
    private AttributeEnum attriEnum;

    public StackableDebuff(StackableDebuffData data, Enemy e) : base(data, e)
    {
        maxStacks = data.maxStacks;
        stacks = new Stack<StatModifer>(maxStacks);
        modifer = data.modifer;
        attriEnum =  data.attriEnum;
    }

    public override void DoEffect()
    {
        if(stacks.Count >= maxStacks) 
        {
            if(maxStacksEffect.Equals(typeof(DebuffData))) 
            {
                DebuffData debuff = maxStacksEffect as DebuffData;
                enemy.AddDebuff(debuff);
            }
            system.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear );
            EndEffect();
            return;
        }
    }

    public void AddToStack()
    {
        if(stacks.Count < maxStacks) 
        {
            var newModifer = new StatModifer(modifer.Value , modifer.Type , modifer.Order , modifer.Source );
            enemy.AddStatModifer( newModifer, attriEnum);
            stacks.Push(newModifer);
        } 
    }

    public override IEnumerator Tick()
    {
        timer = System.Diagnostics.Stopwatch.StartNew();
        while(effectLifeTime >= timer.Elapsed.TotalSeconds)
        {
            DoEffect();
        }
        timer.Reset();
        timer.Stop();

        StatModifer modifer = stacks.Pop();
        enemy.RemoveStatModifer(modifer , attriEnum);

        if(stacks.Count == 0) 
        {
            system.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear );
            EndEffect();
        }
        else 
        {
            enemy.StartCoroutine("Tick");
        }
        yield return null;
    }
}