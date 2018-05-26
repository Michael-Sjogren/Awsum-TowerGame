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
				if(GameManager.instance.won) 
				{

                    StartCoroutine(OnGameWin());
                    GameManager.instance.PauseGame();
                }
				else 
				{
                    StartCoroutine(OnGameLost());
                    GameManager.instance.PauseGame();
                }
				
				panelToShow.SetActive(true);
				displayedResult = true;
			}
		}
	}
	public IEnumerator OnGameWin()
	{
		resultTitle.color = winColor;
		resultTitle.SetText("Level Completed!");
		buttonContinue.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
	}

	public IEnumerator OnGameLost()
	{
        resultTitle.color = lostColor;
		resultTitle.SetText("You Lost");
		buttonContinue.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
	}
}
