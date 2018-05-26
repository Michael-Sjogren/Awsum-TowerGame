
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;

// a living entity
public abstract class LivingEntity : Entity, IDamagable, IMoveable, IEffectable, IHasStats
{
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    [HideInInspector]
    private bool isAlive = true;
    public AgentController Controller { get; set; }
    // status effects & cooldowns
    [Header("Status Effects")]
    public List<StatusEffect> statusEffects = new List<StatusEffect>(10);
    [Header("Status Effect Cooldowns")]
    public List<Cooldown> cooldowns = new List<Cooldown>(5);
    // Stats
    [HideInInspector]
    public float Health { get; set; }
    public float MaxHealth { get; set; }

    public Stat MovementSpeed;

    public EnemySpawner spawner;

    public Stat FireResistance;

    public Stat WaterResistance;
  
    public Stat LightningResistance;
 
    public Stat FrostResistance;
   
    public Stat EarthResitance;

    public virtual IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    public override void Start()
    {
        var data = UnitData as LivingEntityData;
        data.Initialize(this);
        
        IsAlive = true;
        Controller = GetComponent<AgentController>();
        UpdateStats();
    }

    public Stat GetStat(AttributeEnum atrributeType)
    {
        Stat stat = null;
        switch (atrributeType)
        {
            case AttributeEnum.MovementSpeed:
                stat = MovementSpeed;
                break;
            case AttributeEnum.LightningResistance:
                stat = LightningResistance;
                break;
            case AttributeEnum.FrostResistance:
                stat = FrostResistance;
                break;
            case AttributeEnum.EarthResitance:
                stat = EarthResitance;
                break;
            case AttributeEnum.FireResistance:
                stat = FireResistance;
                break;
            case AttributeEnum.WaterResistance:
                stat = WaterResistance;
                break;
        }
        return stat;
    }

    private void UpdateCooldowns()
    {
        if (cooldowns.Count > 0)
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < cooldowns.Count; i++)
            {
                Cooldown cd = cooldowns[i];
                cd.UpdateCooldown(deltaTime , this );
            }
        }
    }

    public void Update()
    {
        UpdateCooldowns();
    }

    public virtual void TakeDamage(float amount)
    {
        if (Health - amount <= 0)
        {
            IsAlive = false;
            StartCoroutine(Die(0));
            OnStatChanged();
            return;
        }
        Health -= amount;
        OnStatChanged();
    }

    public virtual void MoveTo(Vector3 position)
    {
        Controller.Move(position);
    }

    public float GetHealth()
    {
        return Health;
    }

    public Stat GetMovementSpeed()
    {
        return MovementSpeed;
    }

    public void UpdateStats()
    {
        OnStatChanged();
    }

    public void AddStatusEffect(StatusEffectData effectData)
    {
        if (effectData == null) return;
        StatusEffectData newEffectData = effectData;
        // check to see if enemy has debuff types that are opposites to each other , if so absorb them into a lesser or greater effect
        var absorbedEffect = ElementComboManager.instance.CanAbsorbEffect(this, newEffectData.elementType);

        if (absorbedEffect != null)
        {
            newEffectData = absorbedEffect;
        }

        for (int i = 0; i < statusEffects.Count; i++)
        {
            StatusEffect e = statusEffects[i];
            if (e.name == newEffectData.name)
            {
                if(e is StackableEffect)
                {
                    var stackEffect = e as StackableEffect;
                    stackEffect.AddToStack(); 
                    return;
                }
                return;
            }
        }

        if (IsDebuffOnCooldown(newEffectData.name))
        {
            Debug.Log(newEffectData.name + " is on cooldown");
            return;
        }

        newEffectData.Initialize(this);

        StatusEffect effect = newEffectData.effect;
        statusEffects.Add(effect);
        effect.BeginEffect();
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

    public void AddStatModifer(StatModifer modifer, AttributeEnum attributeType)
    {
        switch (attributeType)
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

    public void RemoveStatModifer(StatModifer modifer, AttributeEnum attributeType)
    {
        switch (attributeType)
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
}