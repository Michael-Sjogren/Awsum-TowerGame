using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    public string abillityName = "New Abillity";
    [TextArea]
    public string abillityDescription = "Abillity Description";
    public AudioEvent abillitySound;
    public float cooldown = 0f;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbillity();
}
