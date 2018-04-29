using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

public class GUITowerSelect : GUITowerSelectPanel 
{
	public GameObject mainPanel;
    private Tower building;
	private RectTransform rectTransform;
    private Vector2 offset;
	
    void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		mainPanel.SetActive(false);
	}
    public override void OnTowerSelected(Tower tower)
    {
		building = tower;
		offset.x = rectTransform.rect.width + 50f;
		offset.y = rectTransform.rect.height / 2f;
        if(building == null) 
		{
			mainPanel.SetActive(false);
			return;
		}
		Vector2 pos = Camera.main.WorldToScreenPoint(building.transform.position);
		pos -= offset; 
		this.transform.position = pos;
		mainPanel.SetActive(true);
    }


	void Update()
	{
		if(building != null) 
		{
			Vector2 pos = Camera.main.WorldToScreenPoint(building.transform.position);
			pos -= offset;
			this.transform.position = pos;
		}
	}
}
