using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using TowerDefense.Buildings.Placement;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class Tower : Entity
    {

        [Header("Tower Attributes")]
        [SerializeField]
        private GameObject currentTarget = null;
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
        public int Level { get { return level; } private set { level = value; } }

        public TowerPerkTreeData perkTreeData;
        public bool hasPickedPerk = false;
        [HideInInspector]
        public TowerLevel[] levelData;

        private Stopwatch watch = new Stopwatch();

        public float upgradeTime = 2;
        public float buildTime = 5;

        private BuildSpot buildSpot;
        private ILauncher launcher;

        [Header("Progress bar stuff")]

        [SerializeField]
        private GameObject progressbarPrefab;

        [SerializeField]
        private float distanceAboveTower = 2f;
        private HUDTowerProgressBar progressBar;

        public delegate void TowerChanged();
        public event TowerChanged OnTowerChanged = delegate { };

        private UnitSelectionCircle selectionCircle;

        public TargetingMode targetingMode = TargetingMode.Nearest;

        public override void Start()
        {
            base.Start();
            name = data.name;
            launcher = GetComponent<ILauncher>();
            selectionCircle = GetComponent<UnitSelectionCircle>();
        }

        private void Shoot()
        {
            bool isInRange = CheckIfInRangeOfTower(currentTarget.transform.position);
            if (isInRange)
            {
                launcher.Launch(currentTarget);
                reloadCooldown = 1f / FireRate.Value;
            }
            else
            {
                currentTarget = null;
            }
        }

        public void Update()
        {
            SearchForTarget();
            if (currentTarget != null)
            {
                if (reloadCooldown <= 0)
                {
                    Shoot();
                }
            }
            reloadCooldown -= Time.deltaTime;
        }

        private void SearchForTarget()
        {
            if (GameManager.instance.enemies.Count > 0)
            {
                switch (targetingMode)
                {
                    case TargetingMode.Nearest:
                        currentTarget = GetClosestEnemy();
                        break;
                    case TargetingMode.Strongest:
                        currentTarget = GetStrongestEnemy();
                        break;
                    case TargetingMode.Weakest:
                        currentTarget = GetWeakestEnemy();
                        break;
                    case TargetingMode.FirstInRange:
                        currentTarget = GetFirstEnemyInRange();
                        break;
                    default:
                        currentTarget = GetClosestEnemy();
                        break;
                }
            }
        }

        private GameObject GetFirstEnemyInRange()
        {
            GameObject firstEnemyInRange = currentTarget;
            Vector3 currentPos = transform.position;
            if(firstEnemyInRange == null)
            {
                firstEnemyInRange = GetClosestEnemy();
            }

            return firstEnemyInRange;
        }

        private GameObject GetWeakestEnemy()
        {
            GameObject weakestEnemy = null;
            float minHp = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            var enemies = GetEnemiesInRange();
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy e = enemies[i];
                if (e == null) return null;
                GameObject o = e.gameObject;
                float hp = e.GetHealth();
                if (minHp > hp)
                {
                    weakestEnemy = o;
                    minHp = hp;
                }
            }
            return weakestEnemy;
        }

        private GameObject GetStrongestEnemy()
        {
            GameObject strongestEnemy = null;
            float maxHp = Mathf.NegativeInfinity;
            var enemies = GetEnemiesInRange();
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy e = enemies[i];
                if (e == null) return null;
                GameObject o = e.gameObject;
                float hp = e.GetHealth();
                if (maxHp < hp)
                {
                    strongestEnemy = o;
                    maxHp = hp;
                }
            }
            return strongestEnemy;
        }

        public List<Enemy> GetEnemiesInRange()
        {
            var enemiesInRange = new List<Enemy>(5);
            for (int i = GameManager.instance.enemies.Count - 1; i >= 0; i--)
            {
                var enemy = GameManager.instance.enemies[i];
                if (CheckIfInRangeOfTower(enemy.transform.position))
                {
                    enemiesInRange.Add(enemy);
                }
            }
            return enemiesInRange;
        }
        public GameObject GetClosestEnemy()
        {
            GameObject closestEnemy = null;
            float minDist = Range.Value;
            Vector3 currentPos = transform.position;

            for (int i = GameManager.instance.enemies.Count - 1; i >= 0; i--)
            {
                Enemy e = GameManager.instance.enemies[i];
                if (e == null) return null;
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

        private bool CheckIfInRangeOfTower( Vector3 relativePos )
        {
            float distance = Vector3.Distance( transform.position , relativePos);
            if (distance <= Range.Value)
            {
                return true;
            }
            return false;
        }

        public PerkLevel GetPerkOptionsForCurrentLevel()
        {
            int length = perkTreeData.perkLevels.Length;
            PerkLevel perkLevel = null;
            for (int i = 0; i < length; i++)
            {
                var level = perkTreeData.perkLevels[i];
                if (level.unlockLevel == Level)
                {
                    return level;
                }
            }
            hasPickedPerk = true;
            return perkLevel;
        }


        public void Upgrade()
        {
            if ((level + 1 <= maxLevel))
            {
                TowerLevel data = levelData[level - 1];
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
            upgradePrice = levelData[level - 1].upgradePrice;
            return upgradePrice;
        }

        public void Sell()
        {
            Player player = PlayerManager.Instance.player;
            player.ReciveMoney(sellPrice);
            buildSpot.IsOccupied = false;
            Destroy(gameObject);
        }

        public void InitializeTower(BuildSpot buildSpot)
        {
            var obj = Instantiate(progressbarPrefab, this.transform);
            obj.transform.localPosition = new Vector3(0, distanceAboveTower, 0);
            progressBar = obj.GetComponent<HUDTowerProgressBar>();
            StartCoroutine(BuildTower());
            this.buildSpot = buildSpot;
        }

        private IEnumerator BuildTower()
        {
            watch.Reset();
            watch.Start();
            enabled = false;
            progressBar.ShowProgressBar();
            while (watch.Elapsed.TotalSeconds < buildTime)
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
            if (range <= 0)
            {
                selectionCircle.Radius = Range.Value;
            }
            else
            {
                selectionCircle.Radius = range;
            }
        }

    }


    public enum TargetingMode
    {
        Nearest,
        Strongest,
        Weakest,
        FirstInRange
    }
}


