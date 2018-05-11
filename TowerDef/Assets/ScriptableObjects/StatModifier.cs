using UnityEngine;

[System.Serializable]
public class StatModifer 
{
    public float Value;
    public StatModType Type = StatModType.Flat;
    [HideInInspector]
    public int Order;
    private StatModifer statModifer;
    public object Source;
    public StatModifer(float value , StatModType type , int order  , object source )
    {
        this.Type = type;
        this.Order = order;
        this.Value = value;
    }
     public StatModifer(float value , StatModType type ) : this(value , type , ((int)type ) , null ) {}
     public StatModifer(float value , StatModType type  , int order ) : this(value , type , order , null ) {}
     public StatModifer(float value , StatModType type , object source ) : this(value , type , ((int)type ) , source ) {}
}

public enum StatModType 
{
    Flat = 100,
    PercentAdd = 200,
    PercentMultiply = 300
}