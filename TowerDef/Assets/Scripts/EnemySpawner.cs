using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	
	// make a delegate that is called every time wavtime variable gets updated
	public TextMeshPro waveTime;
	// make a enemySpawnData scriptable object that holds witch enemies should be spawned
	[SerializeField]
	private EnemySpawnData spawnData;
    private int waveIndex = 0;
	private int totalWaves = 0;
	private float countdown = .5f;
	private bool waveInProgress = false;
	public static List<Enemy> enemies = new List<Enemy>();
	public static int totalEnemies;
    public static int enemiesLeft;
	public Transform spawnPoint;
	public Transform goal;

    void Awake () 
	{
		if(spawnData.spawnList == null || spawnData.spawnList.Length <= 0 )
		{
			throw new System.Exception("Please add elements to the spawnlist in the enemy spawner data object");
		}

		if(goal == null) {
			throw new System.Exception("Goal not set on spawner");
		}
		totalWaves = spawnData.spawnList.Length;
		int enemyCount = 0;

		foreach(WaveSpawningDetails waveDetail in spawnData.spawnList) 
		{
			enemyCount += waveDetail.amount;
		}
		
		totalEnemies += enemyCount;
		Debug.Log("Enemey Count" + enemyCount);
		enemiesLeft = totalEnemies;
	}
	
	void Update ()
    {
	
        if (countdown <= 0f && !waveInProgress)
        {
            if (waveIndex < totalWaves)
            {
                StartCoroutine(SpawnNextWave());
                countdown = spawnData.spawnList[waveIndex].timeAfterWave;
            }
        }

        if (GameManager.instance.hasGameStarted && !waveInProgress)
        {
            countdown -= Time.deltaTime;
            waveTime.SetText(((int)countdown).ToString());
        }

		if(GameManager.instance.hasGameStarted) 
		{
			CheckIfWin();
		}

    }

    private void CheckIfWin()
    {
        if (enemiesLeft <= 0 && PlayerManager.Instance.player.Health > 0)
        {
            GameManager.instance.GameOver(true);
        }
        else if(enemiesLeft > 0 && PlayerManager.Instance.player.Health <= 0)
        {
            GameManager.instance.GameOver(false);
        }
    }

    public IEnumerator SpawnNextWave()
	{
		waveInProgress = true;
		int amount = spawnData.spawnList[waveIndex].amount;
		float spawnGapDelay = spawnData.spawnList[waveIndex].timeBetweenSpawn;
		for (int i = 0; i < amount; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(spawnGapDelay);
		}
		waveInProgress = false;
		waveIndex++;
	}

	public void SpawnEnemy()
	{
		GameObject enemyPrefab = spawnData.spawnList[waveIndex].enemyPrefab;
		Vector3 pos = spawnPoint.position;
		Enemy enemy = Instantiate( enemyPrefab  ,  pos , spawnPoint.rotation ).GetComponent<Enemy>();
		enemy.target = goal;
		enemy.spawner = this;
		EnemySpawner.enemies.Add(enemy);
	}

    public void Remove(Enemy enemy)
    {
		enemiesLeft--;
		EnemySpawner.enemies.Remove(enemy);
    }
}
