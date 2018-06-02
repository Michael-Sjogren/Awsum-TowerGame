using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using TowerDefense.Buildings.Placement;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings {
public class Tower : Entity {
		
		[Header("Tower Attributes")]
        [SerializeField]
		private GameObject currentTarget = null;
		public IPlacementArea placementArea { get; private set; }
		/// Gets the grid position for this tower on the 
		public Vector2Int gridPosition { get; private set; }
        public Vector2Int dimensions { get; set; }

		private float reloadCooldown = 0;
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

		public TowerPerkTreeData perkTreeData;
        public bool hasPickedPerk = false;
		[HideInInspector]
		public TowerLevel[] levelData;

		private Stopwatch watch = new Stopwatch();

		public float upgradeTime = 2;
		public float buildTime = 5;

		private ILauncher launcher;

        [Header("Progress bar stuff")]

        [SerializeField]
        private GameObject progressbarPrefab;

        [SerializeField]
        private float distanceAboveTower = 2f;
        private HUDTowerProgressBar progressBar;

        public delegate void TowerChanged();
        public event TowerChanged OnTowerChanged = delegate {};

        private UnitSelectionCircle selectionCircle;

        public override void Start()
		{
			base.Start();
			name = data.name;
			launcher = GetComponent<ILauncher>();
            selectionCircle = GetComponent<UnitSelectionCircle>();
        }

        private void Shoot()
		{
			bool isInRange = CheckIfInRange( currentTarget.transform.position , transform.position);
			if(isInRange) 
			{
				launcher.Launch( currentTarget );
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
				//AimAtTarget();
				if(reloadCooldown <= 0) 
				{
					Shoot();
				}
			} 
			reloadCooldown -= Time.deltaTime;
		}

		private void SearchForTarget()
		{
			if(GameManager.instance.enemies.Count > 0) 
			{
				currentTarget = GetClosestEnemy();
			}
		}
		private void AimAtTarget()
		{
			/* 
			if(stand != null) 
			{
				Vector3 dir = currentTarget.transform.position - transform.position;
				Quaternion lookRotation = Quaternion.LookRotation(dir);
				Vector3 rotation = Quaternion.Lerp( stand.transform.rotation , lookRotation , Time.deltaTime ).eulerAngles;
				stand.transform.rotation = Quaternion.Euler(-90f , rotation.y , 0 );
			}
			*/
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

        public PerkLevel GetPerkOptionsForCurrentLevel()
        {
            int length = perkTreeData.perkLevels.Length;
            PerkLevel perkLevel = null;
            for ( int i = 0; i < length; i++ )
            {
                var level = perkTreeData.perkLevels[i];
                if(level.unlockLevel == Level)
                {
                    return level;
                }
            }
            hasPickedPerk = true;
            return perkLevel;
        }

        public GameObject GetClosestEnemy()
		{
			GameObject closestEnemy = null;
			float minDist = Range.Value;
			Vector3 currentPos = transform.position;

			for (int i = GameManager.instance.enemies.Count - 1; i >= 0; i--)
			{
				Enemy e = GameManager.instance.enemies[i];
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
			if( (level + 1 <= maxLevel) ) 
			{
					TowerLevel data = levelData[level-1];
					level++;
					Range.AddModifer(data.RangeIncrease);
					Damage.AddModifer(data.DamageIncrease);
					FireRate.AddModifer(data.FireRateIncrease);
                    hasPickedPerk = false;
                    OnTowerChanged();
                    UpdateSelectionCircleRadius();
			}
		}
        public TowerLevel GetUpgradeData()
        {
            if (level - 1 >= levelData.Length)
            {
                return null;
            }

            return levelData[level - 1];
        }
        public int GetUpgradePrice()
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

				targetArea.Occupy(destination, dimensions);

                var obj = Instantiate(progressbarPrefab, this.transform);
                obj.transform.localPosition = new Vector3(0, distanceAboveTower, 0);
                progressBar = obj.GetComponent<HUDTowerProgressBar>();

                StartCoroutine(BuildTower());
			}
		}

        private IEnumerator BuildTower()
        {
            watch.Reset();
			watch.Start();
			enabled = false;
            progressBar.ShowProgressBar();
			while(watch.Elapsed.TotalSeconds < buildTime )
			{
				float fillAmount = (float)watch.Elapsed.TotalSeconds / buildTime;
                progressBar.SetFillAmount(fillAmount);
                yield return null;
			}
            progressBar.HideProgressBar();
            this.enabled = true;
        }

        public void AddPerk(PerkOption perk)
		{
			Damage.AddModifer(perk.DamageIncrease);
			Range.AddModifer(perk.RangeIncrease);
			FireRate.AddModifer(perk.FireRateIncrease);
            UpdateSelectionCircleRadius();
            // give a new launcher instead
            if (perk.newProjectile != null) 
			{
				// add component / remove
				//projectile = perk.newProjectile;
			}
            OnTowerChanged();
        }

        public void UpdateSelectionCircleRadius(float range = -1)
        {
            if(range <= 0)
            {
                selectionCircle.Radius = Range.Value;
            }
            else
            {
                selectionCircle.Radius = range;
            }
        }
		
    }
}
    

