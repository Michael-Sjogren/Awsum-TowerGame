
using System.Collections;
using UnityEngine;

public class ParticleVisualEffect : VisualEffect
{
    private ParticleSystem ps;
    public ParticleSystemStopBehavior stopBehavior = ParticleSystemStopBehavior.StopEmittingAndClear;
    // Use this for initialization

    public override void Play(bool destroy)
    {
        GetComponent<ParticleSystem>().Play();
    }

    public override IEnumerator Stop(float delay, ParticleSystemStopAction stopAction)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<ParticleSystem>().Stop(true , stopBehavior);
    }

}
