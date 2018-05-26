using Assets.ScriptableObjects.StatusEffectData;
using Effects;

public interface IEffectable
{
    void AddStatusEffect(StatusEffectData effectData);
    void RemoveStatusEffect(StatusEffect effect);
}