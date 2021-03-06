using UnityEngine;

public abstract class LivingEntityData : UnitData
{
    
    [Header("Stats & Attributes")]
    public float health;
    public float moveSpeed;

    public float armor;
    public float fireResistance;
    public float waterResistance;
    public float lightningResistance;
    public float frostResistance;
    public float earthResitance;
    public override void Initialize(Entity entity)
    {
        var e = entity as LivingEntity;
        e.Health = health;
        e.MaxHealth = health;

        e.MovementSpeed.BaseValue = moveSpeed;
        e.FireResistance.BaseValue = fireResistance;
        e.EarthResitance.BaseValue = earthResitance;
        e.WaterResistance.BaseValue = waterResistance;
        e.LightningResistance.BaseValue = lightningResistance;
    }
}