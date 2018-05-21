using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{
    public bool hasGameStarted = false;
    public bool isCameraRotatable = true;
    public bool isInBuildMode = false;
    public bool isPaused = false;
	
	public bool gameOver = false;
    public bool won = false;
    public int enemiesLeft = 0;

    void Start()
	{
		UnPauseGame();
	}

    public void DisableCameraRotation()
    {
        isCameraRotatable = false;
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
