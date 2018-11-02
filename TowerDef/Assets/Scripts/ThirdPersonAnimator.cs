using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonAnimator : MonoBehaviour 
{
	public Animator animator;
	public NavMeshAgent agent;
	public string forwardName = "Forward";
	public string turnName = "Turn";
	private bool moving;
	// Use this for initialization
	void Start () 
	{
		if(animator == null)
			throw new Exception("Animator is null please set the animator");
		if(agent == null)	
			throw new Exception("NavAgent is null please set the navmesh agent");	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 velocity = agent.desiredVelocity;
		Vector3 move = transform.InverseTransformDirection(velocity.normalized);
		
		//float forwardAmount = Mathf.Repeat( animator.GetCurrentAnimatorStateInfo(0).normalizedTime + .2f, 1);
		float turnAmount = Mathf.Atan2(move.x, move.z);
		animator.SetFloat("Forward", move.normalized.z , 0.12f, Time.deltaTime);
		animator.SetFloat("Turn", turnAmount, 0.12f, Time.deltaTime);
        if (agent.velocity.Equals(Vector3.zero))
        {
            moving = false;
        }
        else if (!agent.velocity.Equals(Vector3.zero))
        {
            moving = true;
        }
        
		animator.SetBool("Moving", moving);
		//animator.SetBool("OnGround", m_IsGrounded);
	}
}
