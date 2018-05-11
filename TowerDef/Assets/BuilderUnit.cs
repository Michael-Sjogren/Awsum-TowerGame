using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuilderUnit : LivingEntity {

	// Use this for initialization
	public GameObject spawnPoint;

	void Start () 
	{
		var agent = GetComponent<NavMeshAgent>();
		agent.Warp(spawnPoint.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
