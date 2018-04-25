using System;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData {
    public abstract class StatusEffectData : ScriptableObject
    {
        [Header("Basic Info")]
        public new string name = "New Effect";

        [Tooltip("The time before the next status effect of the same type can be applied")]
        public float reapplyCooldown;
        
        [Tooltip("This can be unassigned , it will count as nothing")]
        public ElementType elementType;

        [Tooltip("Particle effect prefab")]
        public ParticleSystem particleEffectPrefab;
        public EffectCombonation [] possibleCombonations;
        [HideInInspector]
        public StatusEffect effect = null;

        public abstract void Initialize(Enemy e);

        public StatusEffectData TryCombiningEffects(Enemy enemy)
        {
            if (this.possibleCombonations.Length <= 0)
            {
                Debug.Log("No Combinable effects for this effect was found");
            }
            else
            {
                // check if the enemy has any of the other possible effects on him
                foreach (EffectCombonation effectCombo in this.possibleCombonations)
                {
                    foreach (var effect in enemy.statusEffects)
                    {
                        if (effectCombo.effectB.name == effect.name)
                        {
                            Debug.Log(effectCombo.resultEffect.name);
                            return effectCombo.resultEffect;
                        }
                    }
                }
            }
            return null;
        }
    }

}