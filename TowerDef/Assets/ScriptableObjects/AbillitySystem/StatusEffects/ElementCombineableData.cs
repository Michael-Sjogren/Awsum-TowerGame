using Assets.ScriptableObjects.StatusEffects;
using ScriptableObjects.Enums;
using UnityEngine;

[CreateAssetMenu(menuName="New Element Combine Data" , fileName="Element Combos")]
public class ElementCombineableData : ScriptableObject 
{
    public ElementGroup[] elementData;
}

[System.Serializable]
public struct ElementGroup 
{
    public string elementName;
    public ElementType element;
    public OppositeGroup[] opposites;
}

[System.Serializable]
public struct OppositeGroup
{
    public string oppositeElementName;
    public ElementType oppositeElement;
    public StatusEffect resultingEffect;
}