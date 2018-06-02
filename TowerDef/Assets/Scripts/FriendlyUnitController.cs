using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitController : MonoBehaviour 
{

	public UnitSelectionSystem selectionSystem;

	public string friendlyTag = "Friendly";
    public LayerMask walkableLayer;
	private LivingEntity friendly;
	private AgentController controller;
	private Vector3 target = new Vector3();
	private Vector3 lastPos = new Vector3();
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private AudioEvent heroPositiveResponses;
    private Coroutine routine;

    // Use this for initialization
    void Start () 
	{
        friendly = PlayerManager.Instance.player.GetComponent<LivingEntity>();
        controller = friendly.GetComponent<AgentController>();
        selectionSystem.OnUnitChanged += OnUnitSelected;
        friendly.GetComponent<Entity>().UpdateSelectionCiricle(true);
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
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;
		if(Physics.Raycast(ray , out rayHit, 100f , walkableLayer )) 
		{
			Vector3 newPos = rayHit.point;
            if(routine == null)
            {
               routine = StartCoroutine(PlayHeroResponse());
            }
            lastPos = target;
            target = newPos;

        }else
        {
            controller.StopMoving();
        }
    }

    private IEnumerator PlayHeroResponse()
    {
        heroPositiveResponses.Play(friendly.GetComponent<AudioSource>());
        yield return new WaitForSeconds(7);
        routine = null;
    }
    void OnDisable()
	{
		selectionSystem.OnUnitChanged -= OnUnitSelected;
	}
}
