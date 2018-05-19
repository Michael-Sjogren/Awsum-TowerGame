using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitController : MonoBehaviour {

	public UnitSelectionSystem selectionSystem;

	public string friendlyTag = "Friendly";
    public LayerMask walkableLayer;
	private LivingEntity friendly;
	private AgentController controller;
	private Vector3 target = new Vector3();

    // Use this for initialization
    void Start () 
	{
		selectionSystem.OnUnitChanged += OnUnitSelected;
	}

	public void OnUnitSelected(LivingEntity entity)
	{
		if(entity == null) return;
		if(entity.CompareTag(friendlyTag)) 
		{	
			if(friendly != entity) 
			{
				Debug.Log("Friendly Selected");
				friendly = entity;
				controller = friendly.GetComponent<AgentController>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(1)) 
		{
			GetMouseClickTarget();
				
		}
		if(friendly != null) 
		{
			controller.Move(target);
		}
	}

    private void GetMouseClickTarget()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;
		if(Physics.Raycast(ray , out rayHit, 100f , walkableLayer )) 
		{
			Vector3 newPos = rayHit.point;
			target = newPos;
		}
    }

    void OnDisable()
	{
		selectionSystem.OnUnitChanged -= OnUnitSelected;
	}
}
