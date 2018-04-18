using System.Collections;
using System.Collections.Generic;
using EffectData;
using UnityEngine;

[CreateAssetMenu(fileName="New Tower" , menuName="Tower")]
public class TowerData : UnitData 
{
	public float fireRate; 
	public int buyCost; 
	public int sellPrice;
	public float range;
}
