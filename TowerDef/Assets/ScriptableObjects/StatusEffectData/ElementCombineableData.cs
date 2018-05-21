using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

[CreateAssetMenu(menuName="New Element Combine Data" , fileName="Element Combos")]
public class ElementCombineableData : ScriptableObject 
{
    public ElementGroup[] elementData;
}

[System.Serializable]
public class ElementGroup 
{
    public string name = "Element";
    public ElementType element;
    public OppositeGroup[] opposites;
}

[System.Serializable]
public class OppositeGroup
{
    public string name = "Opposite Element";
    public ElementType oppositeElement;
    public DebuffData resultingEffect;
}