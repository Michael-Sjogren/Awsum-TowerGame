
using System.Collections.Generic;
using ScriptableObjects.Enums;
using UnityEngine;
using System;

namespace EffectData {
    public abstract class StatusEffectData : ScriptableObject
    {
        [Header("Basic Info")]
        public new string name = "New Effect";
        
        [Tooltip("This can be unassigned , it will count as nothing")]
        public ElementType elementType;

        [Tooltip("Particle effect prefab")]
        public ParticleSystem particleEffectPrefab;
        [Tooltip("The time before the next status effect of the same type can be applied")]
        public float reapplyCooldown = .5f;

        public EffectCombonation [] possibleCombonations;

        public abstract void BeginEffect(Enemy e , ParticleSystem particleSystem );
        public virtual void EndEffect(Enemy e)
        {
            RemoveSelf(e);
        }
        public abstract void UpdateEffect(Enemy e , float deltaTime );

        public virtual void RemoveSelf(Enemy e)
        {
            e.RemoveEffect(this);
        }
        public virtual void Initialize(Enemy e) 
        {
            var system = Instantiate
            ( 
                particleEffectPrefab ,
                e.transform.position ,
                particleEffectPrefab.transform.rotation ,
                e.transform
            );
            system.Play(true);
            BeginEffect( e , system );
        }

        public StatusEffectData TryCombiningEffects(Enemy e )  
        {

            if(possibleCombonations.Length <= 0) 
            {
                Debug.Log("No Combinable effects for this effect was found");
            }
            else 
            {   
                // check if the enemy has any of the other possible effects on him
                StatusEffectData effectA = this;
                for (int i = e.effects.Count - 1; i >= 0 ; i--)
                {
                    StatusEffectData effect = e.effects[i];
                    foreach(EffectCombonation effectCombo  in possibleCombonations) 
                    {
                        if(effectCombo.effectB == effect) 
                        {
                            // terminate effect A and effect
                            effect.EndEffect(e);
                            effectA.EndEffect(e);
                            // initialize the combined effect
                            Debug.Log("Creating combined effect: " + effectCombo.resultEffect.name + "\n   From: " + effectA.name + " And "+ effect.name  );
                            return effectCombo.resultEffect;
                        }
                    }
                }
            }
            return null;
        }

        [Serializable]
        public class EffectCombonation 
        {
            public StatusEffectData effectB;
            public StatusEffectData resultEffect;
        }
    }

}