
using UnityEngine;

public abstract class Entity : MonoBehaviour , IUnit
{
    public GameObject selectionCiricle;
    public GameObject SelectionCiricle {get {return selectionCiricle;} set{ selectionCiricle = value; } }
    public bool IsSelected { get; set; }
    public UnitData UnitData { get {return data;} set{ data = value; } }
    public UnitData data;

    public virtual void Start()
    {
        UpdateSelectionCiricle(false);
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
                SelectionCiricle.SetActive(true); 
        }
        else 
        {
            if(selectionCiricle != null)
                SelectionCiricle.SetActive(false);
        }
    }
    
}