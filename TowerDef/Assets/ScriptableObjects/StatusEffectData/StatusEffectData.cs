using System;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData {
    public abstract class StatusEffectData : ScriptableObject
    {
        [Header("Basic Info")]
        public new string name = "New Effect";

        [HideInInspector]
        public StatusEffect effect = null;

        //public abstract void Initialize(Enemy e);
    }

}