

using System.Collections;
using UnityEngine;

public abstract class VisualEffect : MonoBehaviour
{
    public float lifeTime = 0;

    public abstract void Play(bool destroy);
    public abstract IEnumerator Stop(float delay , ParticleSystemStopAction stopAction);
}
