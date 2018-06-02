using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;

[RequireComponent(typeof(LivingEntity))]
public class StatusEffectSystem : MonoBehaviour, IEffectable
{
    private LivingEntity entity;
    [SerializeField]
    private List<StatusEffect> statusEffects;
    [SerializeField]
    private List<Cooldown> cooldowns;

    public List<StatusEffect> StatusEffects { get { return statusEffects; } private set { } }
    public List<Cooldown> Cooldowns { get { return cooldowns; } private set { } }


    // Use this for initialization
    void Start()
    {
        entity = GetComponent<LivingEntity>();
        statusEffects = new List<StatusEffect>();
        cooldowns = new List<Cooldown>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCooldowns();
    }

    public void RegisterDebuffCooldown(Cooldown cooldown)
    {
        if (!CheckIfCooldownExists(cooldown.effectName))
        {
            cooldowns.Add(cooldown);
        }
    }

    public bool CheckIfCooldownExists(string effectName)
    {
        for (int i = 0; i < cooldowns.Count; i++)
        {
            Cooldown cd = cooldowns[i];
            if (cd.effectName == effectName)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsDebuffOnCooldown(string name)
    {
        if (CheckIfCooldownExists(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddStatusEffect(StatusEffectData effectData)
    {
        if (effectData == null) return;
        StatusEffectData newEffectData = effectData;
        // check to see if enemy has debuff types that are opposites to each other , if so absorb them into a lesser or greater effect
        var absorbedEffect = ElementComboManager.instance.CanAbsorbEffect( this , newEffectData.elementType);

        if (absorbedEffect != null)
        {
            newEffectData = absorbedEffect;
        }

        for (int i = 0; i < statusEffects.Count; i++)
        {
            StatusEffect e = statusEffects[i];
            if (e.name == newEffectData.name)
            {
                if (e is StackableEffect)
                {
                    var stackEffect = e as StackableEffect;
                    stackEffect.AddToStack();
                    return;
                }
                return;
            }
        }

        if (IsDebuffOnCooldown(newEffectData.name))
        {
            Debug.Log(newEffectData.name + " is on cooldown");
            return;
        }

        newEffectData.Initialize(entity);

        StatusEffect effect = newEffectData.effect;
        statusEffects.Add(effect);
        effect.BeginEffect();
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        if (statusEffects.Remove(effect))
        {
            //    Debug.Log("Removed effect: " + statusEffect.name);
        }
        else
        {
            //    Debug.Log("Couldnt remove effect , didnt exist");
        }
    }

    private void UpdateCooldowns()
    {
        if (cooldowns.Count > 0)
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < cooldowns.Count; i++)
            {
                Cooldown cd = cooldowns[i];
                cd.UpdateCooldown(deltaTime, this);
            }
        }
    }
}
