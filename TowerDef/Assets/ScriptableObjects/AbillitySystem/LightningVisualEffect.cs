using DigitalRuby.ThunderAndLightning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningVisualEffect : VisualEffect
{
    public LightningBoltPathScript lightning;
    public List<GameObject> targets;
 
    public override void Play(bool destroy)
    {
        lightning.Camera = GameManager.instance.cam;
        lightning.LightningPath = targets;
        if (lightning.ManualMode)
        {
            lightning.Trigger(lifeTime);
        }
        var stopAction = destroy ? ParticleSystemStopAction.Destroy : ParticleSystemStopAction.Disable;
        StartCoroutine(Stop(lifeTime , stopAction));
    }

    public override IEnumerator Stop(float delay , ParticleSystemStopAction stopAction)
    {
        yield return new WaitForSeconds(delay);
        switch (stopAction)
        {
            case ParticleSystemStopAction.Destroy:
                Destroy(this.gameObject);
                break;
            case ParticleSystemStopAction.Disable:
                 
                break;
        }
    }
}
