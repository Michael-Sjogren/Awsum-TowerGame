using System;
using UnityEngine;

public abstract class UnitData : ScriptableObject 
{
    [Header("Unit Attributes")]
    public new string name;
    public Sprite unitIcon;
    public virtual void Initialize(Entity entity)
    {
        entity.name = name;
    }

}