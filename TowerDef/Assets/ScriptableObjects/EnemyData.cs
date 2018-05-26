using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;
[CreateAssetMenu(fileName="New Enemy" , menuName="Enemy/New Enemy")]
public class EnemyData : LivingEntityData
{   

    [Header("Enemy Stuff")]
    public int dropReward;

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        var e = entity as Enemy;
        e.goldDropReward = dropReward;
    }
}