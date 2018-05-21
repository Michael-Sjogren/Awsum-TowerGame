using System.Collections;
using UnityEngine;

namespace Assets.ScriptableObjects.StatusEffectData 
{
    public abstract class TimedEffectData : DebuffData
    {
        [Header("Effect Settings")]
        [Tooltip("How long in seconds the effect will persist on the target")]
        public float effectLifeTime;
        [Tooltip("The amount of times the effect will be invoked during its lifetime \n, if zero the effect will not call 'DoEffect'" +
        " \n, if it is 1 it will call DoEffect once and then destroy itself when the lifetime is over")] 
        [Range( 1 , 500 )]
        public int tickAmount;
    }
}