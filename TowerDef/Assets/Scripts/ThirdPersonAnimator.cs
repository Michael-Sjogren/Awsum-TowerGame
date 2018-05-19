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
		Vector3 move = agent.desiredVelocity;
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		
		//float forwardAmount = Mathf.Repeat( animator.GetCurrentAnimatorStateInfo(0).normalizedTime + .2f, 1);
		float turnAmount = Mathf.Atan2(move.x, move.z);
		animator.SetFloat("Forward", move.z , 0.1f, Time.deltaTime);
		animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
		//animator.SetBool("Crouch", m_Crouching);
		//animator.SetBool("OnGround", m_IsGrounded);
	}
}
