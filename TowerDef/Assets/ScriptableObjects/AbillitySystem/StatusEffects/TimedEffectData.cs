using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData 
{
    public abstract class TimedEffectData : DebuffData
    {
        [Header("Effect Settings")]
        [Tooltip("How long in seconds the effect will persist on the target")]
        public float effectLifeTime;
    }
}