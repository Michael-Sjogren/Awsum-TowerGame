using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

[CreateAssetMenu(fileName="New Tower" , menuName="Tower")]
public class TowerData : UnitData 
{
	public float fireRate; 
	public int buyCost; 
	public int sellPrice;
	public float range;

	public override void Initialize(Entity entity)
	{
		Tower tower = entity as Tower;
		tower.fireRate = fireRate;
		tower.range = range;
		tower.buyCost = buyCost;
		tower.sellPrice = sellPrice;
	}
}
