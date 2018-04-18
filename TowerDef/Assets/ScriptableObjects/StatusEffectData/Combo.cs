using System;
using ScriptableObjects.Enums;
using UnityEngine;

namespace EffectData 
    {
        [Serializable]
        public struct Combo 
        {
            public ElementType EffectA;

            public ElementType EffectB;

            [SerializeField]
            private StatusEffectData createdEffect;
        }
    }