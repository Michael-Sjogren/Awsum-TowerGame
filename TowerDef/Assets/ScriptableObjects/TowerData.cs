using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

[CreateAssetMenu(fileName="New Tower" , menuName="Tower")]
public class TowerData : UnitData 
{
	public int buyCost; 
	public int sellPrice;
	[HideInInspector]
	public int maxLevel; 
	[Header("Tower Stats")]
	public float Damage;
	public float FireRate;
	public float Range;
	[Header("Level Data")]
	public TowerLevelData towerLevelData;
	public TowerPerkTreeData towerPerkTreeData;
	public override void Initialize(Entity entity)
	{
		Tower tower = entity as Tower;
		tower.maxLevel = towerLevelData.levelData.Length;
		tower.levelData = towerLevelData.levelData;
		tower.perkTreeData = towerPerkTreeData;
		tower.FireRate.BaseValue = FireRate;
		tower.Range.BaseValue = Range;
		tower.Damage.BaseValue = Damage;

		tower.buyCost = buyCost;
		tower.sellPrice = sellPrice;
		tower.selectionCiricle.radius = Range;

		tower.selectionCiricle.UpdateCircle();
	}
}
