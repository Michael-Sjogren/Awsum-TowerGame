
using UnityEngine;

public abstract class Entity : MonoBehaviour , IUnit
{
    public UnitSelectionCircle selectionCiricle;
    public UnitSelectionCircle SelectionCiricle {get {return selectionCiricle;} set{ selectionCiricle = value; } }
    public bool IsSelected { get; set; }
    public UnitData UnitData { get {return data;} set{ data = value; } }
    public UnitData data;

    public virtual void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        UnitData.Initialize(this);
    }

    public virtual void UpdateSelectionCiricle(bool isSelected)
    {
        IsSelected = isSelected;
        if(IsSelected)
        {
            if(selectionCiricle != null)
                SelectionCiricle.EnableCiricle();
        }
        else 
        {
            if(selectionCiricle != null)
                SelectionCiricle.DisableCiricle();
        }
    }
    
}