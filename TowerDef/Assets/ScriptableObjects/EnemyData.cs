using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;
[CreateAssetMenu(fileName="New Enemy" , menuName="Enemy/New Enemy")]
public class EnemyData : UnitData
{   

    [Header("Enemy Stuff")]
    public float health;
    public float moveSpeed;
    public float armor;
    public float magicArmor;
    public int dropReward;
    public List<ElementType> weaknesses;

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        Enemy e = entity as Enemy;
        e.health = health;
        e.MaxHealth = health;
        e.MovementSpeed = moveSpeed;
    }
}