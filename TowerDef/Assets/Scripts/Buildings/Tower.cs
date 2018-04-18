using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Buildings.Placement;
using UnityEngine;
namespace Buildings {
public class Tower : Unit {
		
		[Header("Tower Attributes")]
		public TowerData data;
		private float reloadCooldown = 0;
		private GameObject currentTarget = null;

		public IPlacementArea placementArea { get; private set; }

		/// <summary>
		/// Gets the grid position for this tower on the <see cref="placementArea"/>
		/// </summary>
		public Vector2Int gridPosition { get; private set; }
		public GameObject bulletPrefab;
		public Transform firePoint;
		private GameObject stand;
        public Vector2Int dimensions { get; set; }

        // Use this for initialization
        private void Shoot()
		{
			bool isInRange = CheckIfInRange( currentTarget.transform.position , transform.position);
			if(isInRange) 
			{
				GameObject projectile = Instantiate ( bulletPrefab , firePoint.transform.position , firePoint.rotation ) as GameObject;
				projectile.GetComponent<Projectile>().SetTarget( currentTarget );
				reloadCooldown = 1f / data.fireRate;
			}
			else 
			{
				currentTarget = null;
			}
		}
		void Start () 
		{
			Transform s = this.transform.Find("Stand");
			if(s != null)
				stand = s.gameObject;
			this.name = data.name;
		}
		
		// Update is called once per frame
		public void Update () 
		{
			if(currentTarget == null) SearchForTarget();
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
			if(distance <= data.range)
			{
				return true;
			}
			return false;
		}

		GameObject GetClosestEnemy()
		{
			GameObject closestEnemy = null;
			float minDist = data.range;
			Vector3 currentPos = transform.position;

			for (int i = 0; i < EnemySpawner.enemies.Count; i++)
			{
				GameObject o = EnemySpawner.enemies[i].gameObject;
				float dist = Vector3.Distance(o.transform.position, currentPos);
				if (dist < minDist)
				{
					closestEnemy = o;
					minDist = dist;
				}
			}
			return closestEnemy;
		}

		public virtual void Initialize(IPlacementArea targetArea, Vector2Int destination)
        {
			placementArea = targetArea;
			gridPosition = destination;
			if (targetArea != null)
			{
				transform.position = placementArea.GridToWorld(destination, dimensions);
				transform.rotation = placementArea.transform.rotation;
				targetArea.Occupy(destination, dimensions);
			}else {
				Debug.Log("Target area was null");
			}
		}
		
    }
}
    

