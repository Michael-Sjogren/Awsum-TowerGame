using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour 
{
	public GameObject menuPanel;
	public GameObject optionsPanel;
	// Use this for initialization
	void Start () 
	{
		menuPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			ToggleMenu();
		}
	}

    public void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    public void HideOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

	public void ToggleMenu()
	{
		if(!GameManager.instance.isPaused) 
		{
			GameManager.instance.PauseGame();
		}
		else 
		{
			GameManager.instance.UnPauseGame();
		}
		menuPanel.SetActive(!menuPanel.activeSelf);
	}
}
