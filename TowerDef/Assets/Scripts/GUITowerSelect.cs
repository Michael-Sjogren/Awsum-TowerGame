using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

public class GUITowerSelect : GUITowerSelectPanel 
{
	public GameObject mainPanel;
    private Tower currentTower;
	private RectTransform rectTransform;
    private Vector2 offset;
	
    void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		mainPanel.SetActive( false );
	}
    public override void OnTowerSelected(Tower tower)
    {
		currentTower = tower;
		offset.x = rectTransform.rect.width + 50f;
		offset.y = rectTransform.rect.height / 2f;
        if(currentTower == null) 
		{
			mainPanel.SetActive(false);
			return;
		}
		Vector2 pos = Camera.main.WorldToScreenPoint(currentTower.transform.position);
		pos -= offset; 
		this.transform.position = pos;
		mainPanel.SetActive(true);
    }


	void Update()
	{
		if(currentTower != null) 
		{
			Vector2 pos = Camera.main.WorldToScreenPoint(currentTower.transform.position);
			pos -= offset;
			this.transform.position = pos;
		}else 
		{
			if(mainPanel.activeSelf)
				mainPanel.SetActive(false);
		}
	}

	public void Upgrade()
    {
        currentTower.Upgrade();
    }

    public void Sell()
    {
		Debug.Log("Sell");
        currentTower.Sell();
    }
}
