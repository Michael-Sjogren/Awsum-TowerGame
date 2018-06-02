
using Assets.ScriptableObjects.StatusEffectData;
using UnityEngine;

[CreateAssetMenu(menuName="StatusEffects/Debuffs/Stackable Debuff")]
public class StackableEffectData : TimedEffectData
{
    public int maxStacks;
    public StatModifer modifer;
    public AttributeEnum attriEnum;
    public StatusEffectData maxStackEffect;

    public override void Initialize(LivingEntity e)
    {
        effect = new StackableEffect(this , e);
    }
}