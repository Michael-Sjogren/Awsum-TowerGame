using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;
[CreateAssetMenu(fileName="New Enemy" , menuName="Enemy/New Enemy")]
public class EnemyData : UnitData
{   

    [Header("Enemy Attributes")]
    public float health;
    public float moveSpeed;
    public float armor;
    public float magicArmor;
    [Header("Other")]
    public int dropReward;
    public List<ElementType> weaknesses;

    public delegate void OnAttrbuteChanged();
    public OnAttrbuteChanged healthChanged;
    public OnAttrbuteChanged armorChanged;
    public OnAttrbuteChanged magicArmorChanged;
    public OnAttrbuteChanged moveSpeedChanged;
    
    public void Initialize(Enemy enemy)
    {
        enemy.attributes = new Attributes(health , armor , magicArmor , moveSpeed , dropReward );
    }

}