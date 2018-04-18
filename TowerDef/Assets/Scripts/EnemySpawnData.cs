
using System;
using UnityEngine;

[CreateAssetMenu(menuName="Enemy Spawner/New Enemy Spawner Data", fileName="New Enemy Spawner")]
public class EnemySpawnData : ScriptableObject 
{
    public WaveSpawningDetails [] spawnList; 
}

[Serializable]
public class WaveSpawningDetails
{
    public float timeAfterWave = 15f;
    [Tooltip("The delay between each invidual enemy in a wave")]
    public float timeBetweenSpawn;
    [Tooltip("The amount of enemies to be spawned")]
    public int amount;
    public GameObject enemyPrefab;
}