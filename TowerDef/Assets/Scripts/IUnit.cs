

using System;
using System.Collections;
using UnityEngine;
public interface IUnit
{   
    GameObject SelectionCiricle {get;}
    bool IsSelected {get; set;}
    UnitData UnitData {get; set;}
}