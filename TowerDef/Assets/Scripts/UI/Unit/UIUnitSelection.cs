
using System;
using UnityEngine;

public abstract class UIUnitSelection : GUIPanel
{
    void OnEnable()
    {
        selectionSystem.OnUnitChanged += UpdateUI;
    }
    public abstract void UpdateUI(LivingEntity entity);
    void OnDisable()
    {
        selectionSystem.OnUnitChanged -= UpdateUI;
    }
}