using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameResultPanel : MonoBehaviour 
{
	// Use this for initialization
	public TextMeshProUGUI resultTitle;
	public Button buttonContinue;
	public GameObject panelToShow;

	public Color winColor;
	public Color lostColor;
	private bool displayedResult = false;


	void Start()
	{
		panelToShow.SetActive(false);
	}

	public void Update()
	{
		if(GameManager.instanceExists && !displayedResult)
		{
			if(GameManager.instance.gameOver) 
			{
				GameManager.instance.PauseGame();
				if(GameManager.instance.won) 
				{
					OnGameWin();
				}
				else 
				{
					OnGameLost();
				}
				panelToShow.SetActive(true);
				displayedResult = true;
			}
		}
	}
	public void OnGameWin()
	{
		resultTitle.color = winColor;
		resultTitle.SetText("Level Completed!");
		buttonContinue.gameObject.SetActive(true);
	}

	public void OnGameLost()
	{
		resultTitle.color = lostColor;
		resultTitle.SetText("You Lost");
		buttonContinue.gameObject.SetActive(false);
	}
}
