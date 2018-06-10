
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;

// a living entity
public abstract class LivingEntity : Entity, IDamagable, IMoveable, IHasStats
{
    public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
    private bool isAlive = true;
    public AgentController Controller { get; set; }
    // Stats
    [HideInInspector]
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public GameObject centerPosition;

    public Stat MovementSpeed;

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
        OnStatChanged();
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



    public virtual void TakeDamage(float amount)
    {
        if (Health - amount <= 0)
        {
            Health = 0;
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