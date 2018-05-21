
using Assets.ScriptableObjects.StatusEffectData;
using UnityEngine;

public class StackableDebuffData : TimedEffectData
{
    public int maxStacks;
    public StatModifer modifer;
    public AttributeEnum attriEnum;

    public override void Initialize(Enemy e)
    {
        effect = new StackableDebuff(this , e);
    }
}