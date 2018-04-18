using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAgentToTarget : MonoBehaviour 
{
	public Transform target;
	public delegate void AgentHandler();

	// Subscribe to this if you want to get a callback when the ai has reached its destination
	public AgentHandler OnReachedDestination;
	public NavMeshAgent agent;
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () 
	{
		if(target == null) return;
		if(agent.SetDestination(target.position))
		{
			float distance = Vector3.Distance(transform.position , target.position);
			if(distance <= agent.stoppingDistance ) 
			{
				agent.isStopped = true;
				if(OnReachedDestination != null)
					OnReachedDestination();
			}
		}
	}
}
