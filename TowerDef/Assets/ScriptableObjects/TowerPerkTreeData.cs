
using UnityEngine;

[CreateAssetMenu( menuName="Tower/Create New Perk Tree" , fileName="New Tower Perk Tree")]
public class TowerPerkTreeData : ScriptableObject 
{
	public PerkLevel[] perkLevels;
}

[System.Serializable]
public class PerkLevel 
{
	public int unlockLevel;
	public PerkOption perkA;
	public PerkOption perkB;
}

[System.Serializable]
public struct PerkOption 
{
	public StatModifer DamageIncrease;
	public StatModifer RangeIncrease;
	public StatModifer FireRateIncrease;

	[TextArea]
	public string description;

	[Tooltip("If you would like to add a new projectile if this perk is chosen")]
	public Projectile newProjectile;
}
