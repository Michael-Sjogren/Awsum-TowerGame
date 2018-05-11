using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ScriptableObjects.Enums;

[System.Serializable]
public class Stat
{
    public float BaseValue;
    public float Value 
    {
        get 
        {
            if(isDirty || BaseValue != lastBaseValue) 
            {
                lastBaseValue = BaseValue;
                value = GetValue();
                isDirty = false;
            }
            return value;
        }
    }
    private bool isDirty = true;
    private float value;
    private float lastBaseValue = float.MinValue;

    private readonly List<StatModifer> statModifers = new List<StatModifer>();
    public void AddModifer(StatModifer mod) 
    {
        if(mod.Order == 0) {
            mod.Order = (int)mod.Type;
        }
        statModifers.Add(mod);
        statModifers.Sort(CompareModifer);
        isDirty = true;
    }

    public bool RemoveAllModifersFromSource( object source ) 
    {
        bool hasRemoved = false;
        for (int i = statModifers.Count - 1; i >= 0 ; i--)
        {
            var modifer = statModifers[i];
            if(modifer.Source == source)
            {
                statModifers.RemoveAt(i);
                hasRemoved = true;
                isDirty = true;
            }
        }
        return hasRemoved;
    }

    private int CompareModifer(StatModifer a , StatModifer b) 
    {
        if(a.Order < b.Order) 
        {
            return -1;
        }
        else if(a.Order > b.Order) 
        {
            return 1;
        }
        return 0;
    }
    public bool RemoveModifer(StatModifer mod) 
    {
        if(statModifers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    private float GetValue()
    {
        float finalValue = BaseValue;
        float sumPercentage = 0;

        for(int i = 0; i < statModifers.Count; i++)
        {
            var mod = statModifers[i];
            if(mod == null) continue;
            if(mod.Type == StatModType.Flat) 
            {
                finalValue += mod.Value;
            }
            else if(mod.Type == StatModType.PercentAdd) 
            {
                sumPercentage += mod.Value;
                // if im out of bounds or the modifer is not percentadd anymore
                bool outOfBounds = i+ 1 >= statModifers.Count;
                if(outOfBounds) 
                {
                    finalValue *= 1 + sumPercentage;
                    sumPercentage = 0;
                    continue;
                }
                if(statModifers[i+1].Type != StatModType.PercentAdd) 
                {
                    finalValue *= 1 + sumPercentage;
                    sumPercentage = 0;
                }  
            }
            else if(mod.Type == StatModType.PercentMultiply) 
            {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float) Math.Round(finalValue , 2);
    }
}