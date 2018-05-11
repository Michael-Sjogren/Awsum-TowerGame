using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    // create delegeate that the hp bar can listen to a damagable and be updated

    [Header("Enemy")]
    [Header("Pathfinding")]
    // TODO move this to an interface?
    public Transform target;

    [Header("Effects")]
    [SerializeField]
    public List<StatusEffect> statusEffects;
    public Dictionary< string , Cooldown > coolDowns;
    private bool reachedGoal = false;
    private int goldDropReward;

    public override void Initialize()
    {
        Debug.Log("Initialize enemy");
        UnitData.Initialize(this);
        goldDropReward = (UnitData as EnemyData).dropReward;
        IsAlive = true;
        statusEffects = new List<StatusEffect>();
        coolDowns = new Dictionary<string, Cooldown >();
        Controller = GetComponent<AgentController>();
        Controller.OnReachedDestination += ReachedGoal;
    }
    // Hurt the player and die
    private void ReachedGoal()
    {
        if(!reachedGoal) 
        {   
            reachedGoal = true;
            StartCoroutine(Die(0));
            PlayerManager.Instance.player.TakeDamage(1);
            Debug.Log("ReachedGoal");
        }
    }

    private void Attack(IDamagable target, float amount)
    {
        target.TakeDamage(amount);
    }

    public void AddEffect(StatusEffectData effectData)
    {
        if(effectData == null) return;
        StatusEffectData newEffectData = effectData;
        // check to see if enemy has debuff types that are opposites to each other , if so absorb them into a lesser or greater effect
        var absorbedEffect = ElementComboManager.instance.CanAbsorbEffect(this , newEffectData.elementType);

        if(absorbedEffect != null) 
        {
            newEffectData = absorbedEffect;
        }

        foreach(StatusEffect e in statusEffects) 
        {
            if(e.name == newEffectData.name) 
            {
                return;
            }
        }

        if(IsEffectOnCooldown(newEffectData.name)) 
        {
            Debug.Log(newEffectData.name + " is on cooldown");
            return;
        }

        newEffectData.Initialize(this);

        StatusEffect effect = newEffectData.effect;
        statusEffects.Add( effect );
        effect.BeginEffect();
    }

    private StatusEffectData AbsorbEffect(ElementType elementType, ElementType oppositeType)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
       for (int i = coolDowns.Values.Count - 1; i >= 0 ; i--)
       {
           KeyValuePair<string , Cooldown> kv = coolDowns.ElementAt(i);
           if(kv.Value != null)
            kv.Value.UpdateCooldown(Time.deltaTime);
       }

       MoveTo(target.position);
    }
    
    public void RemoveEffect(StatusEffect statusEffect)
    {
        if(statusEffects.Remove(statusEffect))
        {
        //    Debug.Log("Removed effect: " + statusEffect.name);
        }
        else 
        {
        //    Debug.Log("Couldnt remove effect , didnt exist");
        }
    }

    public void RegisterCooldown(string name , Cooldown cooldown)
    {
        if (coolDowns.ContainsKey(name))
        {
            coolDowns[name] = cooldown;
        }
        else
        {
            coolDowns.Add( name, cooldown );
        }
    }

    private bool IsEffectOnCooldown(string name)
    {
        if(!coolDowns.ContainsKey(name))
        {
            coolDowns.Add( name, null );
            return false;
        }
        else 
        {
            return coolDowns[name] != null;
        }
    }


    private void OnDisable()
    {
        Debug.Log(Controller);
        Controller.OnReachedDestination -= ReachedGoal;
    }

    public override IEnumerator Die(float delay)
    {
        IsAlive = false;
        yield return new WaitForSeconds(delay);
        EnemySpawner.enemies.Remove(this);
        if(!reachedGoal) 
        {
            PlayerManager.Instance.player.ReciveMoney(goldDropReward);
        }
        Destroy(this.gameObject);
    }
}
