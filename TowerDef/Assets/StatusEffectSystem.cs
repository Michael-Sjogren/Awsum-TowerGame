using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffects;

using UnityEngine;

[RequireComponent(typeof(LivingEntity))]
public class StatusEffectSystem : MonoBehaviour, IEffectable
{
    private LivingEntity entity;
    [SerializeField]
    private List<StatusEffect> statusEffects;

    [SerializeField]
    private List<Cooldown> cooldowns;

    public List<Cooldown> Cooldowns { get { return cooldowns; } private set { } }
    public List<StatusEffect> StatusEffects { get { return statusEffects; } private set { } }
   
    private Dictionary<StatusEffect, VisualEffect > visualEffects;

    private Dictionary<StatusEffect, Stack<StatModifer>> stackableEffects;


    // Use this for initialization
    void Start()
    {
        entity = GetComponent<LivingEntity>();
        statusEffects = new List<StatusEffect>();
        cooldowns = new List<Cooldown>();
        visualEffects = new Dictionary<StatusEffect, VisualEffect>();
        stackableEffects = new Dictionary<StatusEffect, Stack<StatModifer> >();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldowns();
    }

    public void RegisterDebuffCooldown(Cooldown cooldown)
    {
        if (!CheckIfCooldownExists(cooldown.effectName))
        {
            cooldowns.Add(cooldown);
        }
    }


    public bool CheckIfCooldownExists(string effectName)
    {
        for (int i = 0; i < cooldowns.Count; i++)
        {
            Cooldown cd = cooldowns[i];
            if (cd.effectName == effectName)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsDebuffOnCooldown(string name)
    {
        if (CheckIfCooldownExists(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddStatusEffect(StatusEffect effectData)
    {
        if (effectData == null) return;
        StatusEffect newEffectData = effectData;
        // check to see if enemy has debuff types that are opposites to each other , if so absorb them into a lesser or greater effect
        var absorbedEffect = ElementComboManager.instance.CanAbsorbEffect( this , newEffectData.elementType);

        if (absorbedEffect != null)
        {
            newEffectData = absorbedEffect;
        }

        for (int i = 0; i < statusEffects.Count; i++)
        {
            StatusEffect e = statusEffects[i];
            if (e.name == newEffectData.name)
            {
                if (e is StackableEffect)
                {
                    var stackEffect = e as StackableEffect;
                    AddToStack(stackEffect);
                    return;
                }
                return;
            }
        }

        if (IsDebuffOnCooldown(newEffectData.name))
        {
            return;
        }

        statusEffects.Add(newEffectData);
        newEffectData.BeginEffect(entity);
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        if (statusEffects.Remove(effect))
        {
            //    Debug.Log("Removed effect: " + statusEffect.name);
        }
        else
        {
            //    Debug.Log("Couldnt remove effect , didnt exist");
        }
    }

    private void UpdateCooldowns()
    {
        if (cooldowns.Count > 0)
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < cooldowns.Count; i++)
            {
                Cooldown cd = cooldowns[i];
                cd.UpdateCooldown(deltaTime, this);
            }
        }
    }

    public void AddVisualEffect(StatusEffect key , VisualEffect value )
    {
        if (visualEffects.ContainsKey(key))
        {
            visualEffects[key] = value;
        }
        else
        {
            visualEffects.Add(key, value);
        }
    }


    public void RemoveVisualEffect(StatusEffect key)
    {
        visualEffects.Remove(key);
    }

    public void AddNewStackEffect(StackableEffect stackEffect , int maxStacks )
    {
        Stack<StatModifer> stacks = new Stack<StatModifer>(maxStacks);
        if (!stackableEffects.ContainsKey(stackEffect))
        {
            stackableEffects.Add(stackEffect, stacks);
        }
        else
        {
            stackableEffects[stackEffect] = stacks;
        }

        AddToStack(stackEffect);
    }

    public void RemoveStackEffect(StackableEffect stackEffect)
    {
        stackableEffects.Remove(stackEffect);
    }

    public void AddToStack(StackableEffect stackEffect)
    {
        var modifer = stackEffect.modifer;
        var newModifer = new StatModifer(modifer.Value, modifer.Type, modifer.Order, modifer.Source);

        var stacks = stackableEffects[stackEffect];
        if(stacks != null)
        {
            entity.AddStatModifer(newModifer , stackEffect.attriEnum );
            stacks.Push(newModifer);
        }
    }

    public Stack<StatModifer> GetStacksFromStackEffect(StatusEffect key)
    {
        return stackableEffects[key];
    }

    public VisualEffect GetVisualEffect(StatusEffect key)
    {
        if(visualEffects.ContainsKey(key))
        {
            return visualEffects[key];
        }
        return null;
    }
}
