using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffects
{
    public abstract class TimedEffect : StatusEffect
    {
        [Header("Effect Settings")]
        [Tooltip("How long in seconds the effect will persist on the target")]
        public float effectLifeTime;
    }
}