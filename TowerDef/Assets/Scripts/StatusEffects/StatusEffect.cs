
using System;
using Assets.ScriptableObjects.StatusEffectData;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public abstract class StatusEffect
    {
        public string name;
        public readonly StatusEffectData data;
        public StatusEffect(StatusEffectData data)
        {
            this.data = data;
            this.name = data.name;

        }
        public abstract void BeginEffect();

        public abstract void EndEffect();

    }
}
