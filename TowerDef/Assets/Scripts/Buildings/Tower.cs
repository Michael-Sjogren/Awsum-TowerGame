﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Buildings.Placement;
using UnityEngine;

namespace Buildings {
public class Tower : Entity {
		
		[Header("Tower Attributes")]
		private float reloadCooldown = 0;
		private GameObject currentTarget = null;

		public IPlacementArea placementArea { get; private set; }

		/// <summary>
		/// Gets the grid position for this tower on the <see cref="placementArea"/>
		/// </summary>
		public Vector2Int gridPosition { get; private set; }
		public Projectile projectile;
		public Transform firePoint;
		private GameObject stand;
        public Vector2Int dimensions { get; set; }
		[HideInInspector]
		public Stat FireRate;
		[HideInInspector]
		public Stat Range;
		[HideInInspector]
		public Stat Damage;
		[HideInInspector]
		public int buyCost;
		[HideInInspector]
        public int sellPrice;
		[HideInInspector]
        public int maxLevel;
		[HideInInspector]
		private int level = 1;
		[HideInInspector]
		public int Level {get {return level;} private set { level = value;}}
		[HideInInspector]
		public Dictionary<int , PerkOption> perksSelected = new Dictionary<int, PerkOption>();
		public TowerPerkTreeData perkTreeData;
		public TowerLevel[] levelData;
		public AudioSource fireProjectileAudioSource;
		[HideInInspector]
		public AuidoEvent fireProjectileAudioEvent;
        public override void Initialize()
		{
			UnitData.Initialize(this);
			Transform s = this.transform.Find("Stand");
			if(s != null)
				stand = s.gameObject;
			this.name = data.name;
		}


        private void Shoot()
		{
			bool isInRange = CheckIfInRange( currentTarget.transform.position , transform.position);
			if(isInRange) 
			{
				Projectile p = Instantiate ( projectile , firePoint.transform.position , firePoint.rotation );
				p.Launch( currentTarget , this );

				if(fireProjectileAudioEvent != null)
					fireProjectileAudioEvent.Play(fireProjectileAudioSource);

				reloadCooldown = 1f / FireRate.Value;
			}
			else 
			{
				currentTarget = null;
			}
		}
	
		public void Update () 
		{
			SearchForTarget();
			if(currentTarget != null) {
				AimAtTarget();
				if(reloadCooldown <= 0) 
				{
					Shoot();
				}
			} 
			reloadCooldown -= Time.deltaTime;
		}

		private void SearchForTarget()
		{
			if(EnemySpawner.enemies.Count > 0) 
			{
				currentTarget = GetClosestEnemy();
			}
		}
		private void AimAtTarget()
		{
			if(stand != null) 
			{
				Vector3 dir = currentTarget.transform.position - transform.position;
				Quaternion lookRotation = Quaternion.LookRotation(dir);
				Vector3 rotation = Quaternion.Lerp( stand.transform.rotation , lookRotation , Time.deltaTime ).eulerAngles;
				stand.transform.rotation = Quaternion.Euler(-90f , rotation.y , 0 );
			}
		}

		private bool CheckIfInRange(Vector3 a , Vector3 b)
		{
			float distance = Vector3.Distance(a , b);
			if(distance <= Range.Value)
			{
				return true;
			}
			return false;
		}

		public GameObject GetClosestEnemy()
		{
			GameObject closestEnemy = null;
			float minDist = Range.Value;
			Vector3 currentPos = transform.position;

			for (int i = EnemySpawner.enemies.Count -1; i >= 0; i--)
			{
				Enemy e = EnemySpawner.enemies[i];
				if(e == null) return null;
				GameObject o = e.gameObject;
				float dist = Vector3.Distance(o.transform.position, currentPos);
				if (dist < minDist)
				{
					closestEnemy = o;
					minDist = dist;
				}
			}
			return closestEnemy;
		}

		public void Upgrade()
		{
			Player player = PlayerManager.Instance.player;
			if( (level + 1 <= maxLevel) ) 
			{
				if(player.CanAfford(GetUpgradePrice())) 
				{
					TowerLevel data = levelData[level-1];
					level++;
					player.BuyItem(GetUpgradePrice());

					Range.AddModifer(data.RangeIncrease);
					Damage.AddModifer(data.DamageIncrease);
					FireRate.AddModifer(data.FireRateIncrease);
					OnStatChanged();
				}
			}
		}

        private int GetUpgradePrice()
        {	
			int upgradePrice = 0;
			upgradePrice = levelData[level -1].upgradePrice;
            return upgradePrice;
        }

        public void Sell()
		{
			Player player = PlayerManager.Instance.player;
			player.ReciveMoney(this.sellPrice);
			placementArea.Clear(gridPosition , dimensions);
			Destroy(this.gameObject);
		}

		public virtual void Initialize(IPlacementArea targetArea, Vector2Int destination)
        {
			placementArea = targetArea;
			gridPosition = destination;
			if (targetArea != null)
			{
				transform.position = placementArea.GridToWorld(destination, dimensions);
				transform.rotation = placementArea.transform.rotation;
				Initialize();
				targetArea.Occupy(destination, dimensions);
			}else {
				Debug.Log("Target area was null");
			}
		}


		public void AddPerk(PerkOption perk)
		{
			Damage.AddModifer(perk.DamageIncrease);
			Range.AddModifer(perk.RangeIncrease);
			FireRate.AddModifer(perk.FireRateIncrease);
			if(perk.newProjectile != null) 
			{
				projectile = perk.newProjectile;
			}
		}
		
    }
}
    

