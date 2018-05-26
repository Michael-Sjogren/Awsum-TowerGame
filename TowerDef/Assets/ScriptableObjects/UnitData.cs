using System;
using UnityEngine;

public abstract class UnitData : ScriptableObject 
{
    [Header("Unit Attributes")]
    public new string name;
    public Sprite unitIcon;
    public abstract void Initialize(Entity entity);
}