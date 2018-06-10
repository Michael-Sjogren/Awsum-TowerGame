using System;
using ScriptableObjects.Enums;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData 
    {
        [Serializable]
        public struct Combo 
        {
            public ElementType EffectA;

            public ElementType EffectB;

            [SerializeField]
            private Ability resultingAbillity;
        }
    }