using Assets.ScriptableObjects.StatusEffects;

public interface IEffectable
{
    void AddStatusEffect(StatusEffect effectData);
    void RemoveStatusEffect(StatusEffect effect);
}