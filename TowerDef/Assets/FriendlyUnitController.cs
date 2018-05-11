using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitController : MonoBehaviour {

	public UnitSelectionSystem selectionSystem;

	public string friendlyTag = "Friendly";
    public LayerMask walkableLayer;
	private LivingEntity friendly;

	private Vector3 target;

    // Use this for initialization
    void Start () 
	{
		selectionSystem.OnUnitChanged += OnUnitSelected;
	}

	public void OnUnitSelected(LivingEntity entity)
	{
		
		if(entity.CompareTag(friendlyTag)) 
		{	
			if(friendly != entity) 
			{
				Debug.Log("Friendly Selected");
				friendly = entity;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(friendly != null) 
		{
			if(Input.GetMouseButtonDown(1)) 
			{
				AgentController controller = friendly.GetComponent<AgentController>();
				GetMouseClickTarget();
				controller.Move(target);
			}

		}
	}

    private void GetMouseClickTarget()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;
		if(Physics.Raycast(ray , out rayHit, 500f , walkableLayer )) 
		{
			Debug.Log(rayHit.point);
			target = rayHit.point;
		}
        target = Vector3.negativeInfinity;
    }

    void OnDisable()
	{
		selectionSystem.OnUnitChanged -= OnUnitSelected;
	}
}
