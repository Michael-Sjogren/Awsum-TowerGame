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
    public List<Cooldown> cooldowns;
    public GameObject coinPrefab;
    private bool reachedGoal = false;
    private int goldDropReward;

    public override void Initialize()
    {
        Debug.Log("Initialize enemy");
        UnitData.Initialize(this);
        goldDropReward = (UnitData as EnemyData).dropReward;
        IsAlive = true;

        statusEffects = new List<StatusEffect>(10);
        cooldowns = new List<Cooldown>(10);

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

        for (int i = 0; i < statusEffects.Count; i++) 
        {
            StatusEffect e = statusEffects[i];
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
       if(cooldowns.Count > 0) 
       {
           float deltaTime = Time.deltaTime;
           for( int i = 0; i < cooldowns.Count; i++)
           {
                Cooldown cd = cooldowns[i];
                cd.UpdateCooldown(deltaTime);
           }
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

    public void RegisterCooldown( Cooldown cooldown)
    {
        if(!CheckIfCooldownExists(cooldown.effectName)) 
        {
            cooldowns.Add( cooldown );
        }
    }

    private bool IsEffectOnCooldown(string effectName)
    {

        if(CheckIfCooldownExists(effectName))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool CheckIfCooldownExists(string effectName)
    {
        for(int i = 0; i < cooldowns.Count; i++) 
        {
            Cooldown cd = cooldowns[i];
            if(cd.effectName == effectName) 
            {
                return true;
            }
        }
        return false;
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
            for(int i = 0; i < goldDropReward; i++) 
            {
                Instantiate( coinPrefab , this.transform.position + Vector3.up * 1.5f , Quaternion.identity );
            }
            PlayerManager.Instance.player.ReciveMoney(goldDropReward);
        }
        Destroy(this.gameObject);
    }
}
