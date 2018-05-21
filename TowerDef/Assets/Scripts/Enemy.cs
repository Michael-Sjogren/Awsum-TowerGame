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
    public List<Debuff> debuffs;
    public List<Cooldown> cooldowns;
    public GameObject coinPrefab;
    public bool reachedGoal = false;
    private int goldDropReward;
    [HideInInspector]
    public EnemySpawner spawner;
    [HideInInspector]
    public Stat FireResistance;
    [HideInInspector]
    public Stat WaterResistance;
    [HideInInspector]
    public Stat LightningResistance;
    [HideInInspector]
    public Stat FrostResistance;
    [HideInInspector]
    public Stat EarthResitance;

    public override void Initialize()
    {
        Debug.Log("Initialize enemy");
        UnitData.Initialize(this);
        OnStatChanged(); 
        goldDropReward = (UnitData as EnemyData).dropReward;
        IsAlive = true;

        debuffs = new List<Debuff>(10);
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

    public void AddDebuff(DebuffData effectData)
    {
        if(effectData == null) return;
        DebuffData newDebuffData = effectData;
        // check to see if enemy has debuff types that are opposites to each other , if so absorb them into a lesser or greater effect
        var absorbedEffect = ElementComboManager.instance.CanAbsorbEffect(this , newDebuffData.elementType);

        if(absorbedEffect != null) 
        {
            newDebuffData = absorbedEffect;
        }

        for (int i = 0; i < debuffs.Count; i++) 
        {
            StatusEffect e = debuffs[i];
            if(e.name == newDebuffData.name) 
            {
                if(e.Equals(typeof(StackableDebuff))) 
                {
                    var stackEffect = e as StackableDebuff;
                    stackEffect.AddToStack();
                }
                return;
            }
        }

        if(IsDebuffOnCooldown(newDebuffData.name)) 
        {
            Debug.Log(newDebuffData.name + " is on cooldown");
            return;
        }

        newDebuffData.Initialize(this);

        Debuff effect = newDebuffData.effect as Debuff;
        debuffs.Add( effect );
        effect.BeginEffect();
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
    
    public void RemoveDebuff(Debuff debuff)
    {
        if(debuffs.Remove(debuff))
        {
        //    Debug.Log("Removed effect: " + statusEffect.name);
        }
        else 
        {
        //    Debug.Log("Couldnt remove effect , didnt exist");
        }
    }

    public void RegisterDebuffCooldown( Cooldown cooldown)
    {
        if(!CheckIfCooldownExists(cooldown.effectName)) 
        {
            cooldowns.Add( cooldown );
        }
    }

    private bool IsDebuffOnCooldown(string effectName)
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

    public void AddStatModifer(StatModifer modifer , AttributeEnum attriEnum)
    {
        switch(attriEnum) 
        {
            case AttributeEnum.MovementSpeed:
                this.MovementSpeed.AddModifer(modifer);
            break;
            case AttributeEnum.LightningResistance:
                this.LightningResistance.AddModifer(modifer);
            break;
            case AttributeEnum.FrostResistance:
                this.FrostResistance.AddModifer(modifer);
            break;
            case AttributeEnum.EarthResitance:
                this.EarthResitance.AddModifer(modifer);
            break;
            case AttributeEnum.FireResistance:
                this.FireResistance.AddModifer(modifer);
            break;
            case AttributeEnum.WaterResistance:
                this.WaterResistance.AddModifer(modifer);
            break;
        }
        UpdateStats();
    }

    public void RemoveStatModifer(StatModifer modifer , AttributeEnum attriEnum)
    {
        switch(attriEnum) 
        {
            case AttributeEnum.MovementSpeed:
                this.MovementSpeed.RemoveModifer(modifer);
            break;
            case AttributeEnum.LightningResistance:
                this.LightningResistance.RemoveModifer(modifer);
            break;
            case AttributeEnum.FrostResistance:
                this.FrostResistance.RemoveModifer(modifer);
            break;
            case AttributeEnum.EarthResitance:
                this.EarthResitance.RemoveModifer(modifer);
            break;
            case AttributeEnum.FireResistance:
                this.FireResistance.RemoveModifer(modifer);
            break;
            case AttributeEnum.WaterResistance:
                this.WaterResistance.RemoveModifer(modifer);
            break;
        }
        UpdateStats();
    }

    public override IEnumerator Die(float delay)
    {
        IsAlive = false;
        yield return new WaitForSeconds(delay);
        
        if(!reachedGoal) 
        {
            for(int i = 0; i < goldDropReward; i++) 
            {
                Instantiate( coinPrefab , this.transform.position + Vector3.up * 1.5f , Quaternion.identity );
            }
            PlayerManager.Instance.player.ReciveMoney(goldDropReward);
        }
        Destroy(this.gameObject);
        spawner.Remove(this);
    }
}
