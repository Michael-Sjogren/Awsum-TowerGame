using DigitalRuby.ThunderAndLightning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningVisualEffect : VisualEffect
{
    public LightningBoltPathScript lightning;
    public List<GameObject> targets;
 
    public override void Play()
    {
        lightning.Camera = GameManager.instance.cam;
        lightning.LightningPath = targets;
        StartCoroutine(Stop(.5f));
    }

    public override IEnumerator Stop(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
