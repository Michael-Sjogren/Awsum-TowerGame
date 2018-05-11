﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour 
{
	public delegate void AgentTargetHandler();

	public delegate void AgentAttributeHandler(float amount );

	// Subscribe to this if you want to get a callback when the ai has reached its destination
	public event AgentTargetHandler OnReachedDestination = delegate{};
	private LivingEntity movableEntity;
	private NavMeshAgent agent;

	public float maxStoppingDistanceFromGoal;

    // Use this for initialization
    void Start () {
		agent = GetComponent<NavMeshAgent>();
		movableEntity = GetComponent<LivingEntity>();
		movableEntity.OnStatChanged += OnMovementSpeedUpdated;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void OnMovementSpeedUpdated()
	{
		agent.speed = movableEntity.GetMovementSpeed().Value;
	}

	public void Move(Vector3 target)
	{
		if(agent.SetDestination(target))
		{
			float distance = Vector3.Distance(transform.position , target);
			if(distance <= maxStoppingDistanceFromGoal ) 
			{
				agent.isStopped = true;
				if(OnReachedDestination != null)
					OnReachedDestination();
			}
		}
	}

	void OnDisable()
	{
		movableEntity.OnStatChanged -= OnMovementSpeedUpdated;
	}
}
