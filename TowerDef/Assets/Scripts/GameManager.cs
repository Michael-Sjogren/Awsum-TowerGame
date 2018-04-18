using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{
	public bool gameOver = false;
    public bool hasGameStarted = false;
    public bool isCameraRotatable = true;
    internal bool isInBuildMode = false;

	void Start()
	{
		
	}

    public void DisableCameraRotation()
    {
        isCameraRotatable = false;
    }

    public void EnableCameraRotation()
    {
        isCameraRotatable = true;
    }

    private void Update()
	{
		if(gameOver) 
		{
			SceneManager.LoadScene("Lose");
		}
	}

	public void StartGame()
	{
		hasGameStarted = true;
	}
	
}
