
using System.Collections;
using UnityEngine;
using System;

namespace EffectData 
{
    public abstract class TimedEffectData : StatusEffectData
    {
        [Header("Effect Settings")]
        [Tooltip("How long in seconds the effect will persist on the target")]
        public float effectLifeTime;

        [Tooltip("The amount of times the effect will be invoked during its lifetime")] 
               
        [Range(1 , 300)]
        public int intervalAmount = 1;

        protected float timeGap;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            if(intervalAmount < 0) intervalAmount = 1;
            timeGap = effectLifeTime / intervalAmount;
        }

        public override void BeginEffect(Enemy e , ParticleSystem particleSystem ) 
        {
            e.StartCoroutine(Tick(e , particleSystem ));
        }

        public abstract void DoEffect(Enemy e);

        public virtual IEnumerator Tick(Enemy e  , ParticleSystem particleSystem  , float secondGap = -1)
        {
            if(secondGap < 0) 
            {
                secondGap = timeGap;
            }
            for(int i = 0; i < intervalAmount; i++) 
            {
                if(!e.effects.Contains(this)) 
                {
                    Debug.Log("Waiting for apply cooldown");
                    yield return new WaitForSeconds(reapplyCooldown);
                    yield break;
                }
                DoEffect(e);
                yield return new WaitForSeconds(timeGap);
            }
            particleSystem.Stop(true , ParticleSystemStopBehavior.StopEmitting );
            EndEffect(e);
        }

        public override void EndEffect(Enemy e)
        {
            base.EndEffect(e);
        }
    }
}