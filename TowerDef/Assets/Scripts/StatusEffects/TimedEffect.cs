
using System;
using System.Collections;
using System.Diagnostics;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using UnityEngine;

namespace Effects {
    public abstract class TimedEffect : StatusEffect
    {
        protected float effectLifeTime;
        protected float timeTickGap;
        protected float timeGap;
        protected Stopwatch timer;

        public TimedEffect(TimedEffectData data, Enemy e) : base(data, e) 
        {
            effectLifeTime = data.effectLifeTime;
            reapplyCooldown = data.reapplyCooldown;
            timeGap = data.effectLifeTime / data.tickAmount;
            if(timeGap > effectLifeTime  || timeGap < 0) 
            {
                UnityEngine.Debug.Log("Timegap cannot be less than zero or more than the effect lifetime");
            }
            timeTickGap = Mathf.Clamp(timeGap , 0f , effectLifeTime );
        }

        public override void BeginEffect()
        {
            base.BeginEffect();
            routine = enemy.StartCoroutine(Tick());
        }

        public virtual IEnumerator Tick()
        {
            timer = Stopwatch.StartNew();
            while(effectLifeTime >= timer.Elapsed.TotalSeconds)
            {
                DoEffect();
                yield return new WaitForSeconds(timeTickGap);
            }
            system.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear );
            timer.Stop();
            timer.Reset();
            EndEffect();
        }
        public abstract void DoEffect();

        public override void EndEffect()
        {
            //enemy.StopCoroutine(routine);  
            base.EndEffect();
        }
    }
}