using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abillityName = "New Ability";
    [TextArea]
    public string abillityDescription = "";
    public AudioEvent abillitySound;
    public VisualEffect abillityVisualEffect;
    public abstract void TriggerAbility(GameObject obj , Vector3 center);
    public abstract void StopVisualEffect(VisualEffect vfx);
    public abstract void PlayVisualEffect(VisualEffect effect);
}
