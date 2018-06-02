using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{
    public bool hasGameStarted = false;
    public bool isCameraRotatable = true;
    public bool isInBuildMode = false;
    public bool isPaused = false;
	
	public bool gameOver = false;
    public bool won = false;
    private int enemiesLeft = 0;
    private int totalEnemies;
    public Camera cam;
    [HideInInspector]
    public GameObject coinContainer;
    [HideInInspector]
    public GameObject enemyContainer;
    public List<Enemy> enemies;

    public int TotalEnemies
    {
        get
        {
            return totalEnemies;
        }
        set
        {
            totalEnemies += value;
            enemiesLeft = totalEnemies;
        }
    }

    public int EnemiesLeft
    {
        get
        {
            return enemiesLeft;
        }

        set
        {
            enemiesLeft = value;
            if(enemiesLeft <= 0 && hasGameStarted && PlayerManager.Instance.player.Health > 0)
            {
                GameOver(true);
            }
            else if (enemiesLeft > 0 && hasGameStarted && PlayerManager.Instance.player.Health <= 0)
            {
                GameOver(false);
            }
        }
    }

    void Start()
	{
        enemies = new List<Enemy>(50);
        coinContainer = new GameObject("Coins");
        enemyContainer = new GameObject("Enemies");

        UnPauseGame();
	}

    public void ChangeSoundEffectsVolume(Slider volumeSlider)
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void DisableCameraRotation()
    {
        isCameraRotatable = false;
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    } 

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemies.Remove(enemy))
        {
            EnemiesLeft--;
        }
    }

    public void UnPauseGame()
    {
		Time.timeScale = 1;
        isPaused = false;
    }

    public void PauseGame()
    {
       Time.timeScale = 0;
	   isPaused = true;
    }

    public void EnableCameraRotation()
    {
        isCameraRotatable = true;
    }

	public void StartGame()
	{
		hasGameStarted = true;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void GameOver(bool win)
	{
        gameOver = true;
        this.won = win;
	}
	
}
