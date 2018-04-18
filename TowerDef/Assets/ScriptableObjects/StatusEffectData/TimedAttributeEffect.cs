using System.Collections;
using System.Collections.Generic;
using EffectData;
using ScriptableObjects.Enums;
using UnityEngine;

[CreateAssetMenu(menuName="StatusEffects/Timed Effects/New Attribute effect")]
public class TimedAttributeEffect : TimedEffectData 
{
    public AttributeEnum attributeEnum;
    [Range(0 , 2)]
    [Tooltip("Amount in percentage you want the attribute to be increased or decreased")]
    public float percentageChange = 1;
    public override void BeginEffect(Enemy e, ParticleSystem particleSystem)
    {
        ChangeAttribute(e , percentageChange);
        particleSystem.Play(true);
        base.BeginEffect(e , particleSystem);
    }
    public override void DoEffect(Enemy e)
    {
       
    }

    public override void EndEffect(Enemy e)
    {
        RevertChange(e);
    }
    public override void UpdateEffect(Enemy e, float deltaTime) {}
    private void ChangeAttribute( Enemy e , float percentage )
    {
        switch(attributeEnum)
        {
            case AttributeEnum.Armor :
                e.attributes.armor = percentageChange * e.enemyData.armor;
            break;
            case AttributeEnum.Health :
                e.attributes.maxHealth = percentageChange * e.enemyData.health;
            break;
            case AttributeEnum.MoveSpeed :
                e.attributes.moveSpeed = percentageChange * e.enemyData.moveSpeed;
            break;
            case AttributeEnum.MagicArmor :
                e.attributes.magicArmor =  percentageChange * e.enemyData.magicArmor;
            break;
        }
        e.UpdateEffects();
    }


    private void RevertChange( Enemy e )
    {
        switch(attributeEnum)
        {
            case AttributeEnum.Armor :
                e.attributes.armor = e.enemyData.armor;
            break;
            case AttributeEnum.Health :
                e.attributes.maxHealth = e.enemyData.health;
            break;
            case AttributeEnum.MoveSpeed :
                e.attributes.moveSpeed = e.enemyData.moveSpeed;
            break;
            case AttributeEnum.MagicArmor :
                e.attributes.magicArmor =  e.enemyData.magicArmor;
            break;
        }
        e.UpdateEffects();
    }
}

[SerializeField]
public enum AttributeEnum
{
    Health,
    Armor,
    MagicArmor,
    MoveSpeed
}