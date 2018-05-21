using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;
[CreateAssetMenu(fileName="New Enemy" , menuName="Enemy/New Enemy")]
public class EnemyData : UnitData
{   

    [Header("Enemy Stuff")]
  
    public int dropReward;
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
        base.Initialize(entity);
        var e = entity as Enemy;
        e.Health = health;
        e.MaxHealth = health;

        e.MovementSpeed.BaseValue = moveSpeed;
        e.FireResistance.BaseValue = fireResistance;
        e.EarthResitance.BaseValue = earthResitance;
        e.WaterResistance.BaseValue = waterResistance;
        e.LightningResistance.BaseValue = lightningResistance;
    }
}