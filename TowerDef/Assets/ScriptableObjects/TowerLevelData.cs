
using UnityEngine;

[CreateAssetMenu(menuName="Tower/New Level Data")]
[System.Serializable]
public class TowerLevelData : ScriptableObject
{
    public TowerLevel[] levelData;
}
[System.Serializable]
public class TowerLevel 
{
    public string name = "Level";
    public int upgradePrice;
    
    [Header("Base Attributes")]
    public StatModifer RangeIncrease;
    public StatModifer DamageIncrease;
    public StatModifer FireRateIncrease;
}