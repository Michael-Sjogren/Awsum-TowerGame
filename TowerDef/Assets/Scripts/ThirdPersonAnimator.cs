using System;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonAnimator : MonoBehaviour 
{
	public Animator animator;
	public NavMeshAgent agent;
	public string forwardName = "Forward";
	public string turnName = "Turn";
	private bool moving;
    private Rigidbody body;
	// Use this for initialization
	void Start () 
	{
		if(animator == null)
			throw new Exception("Animator is null please set the animator");
        if (agent == null)
            Debug.Log("Null agent");
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 velocity = Vector3.zero;
        if(agent == null)
        {
            if(body != null)
            {
                velocity = body.velocity;
            }
        }
        else
        {
            velocity = agent.velocity;
        }
		Vector3 move = transform.InverseTransformDirection(velocity.normalized);
		
		//float forwardAmount = Mathf.Repeat( animator.GetCurrentAnimatorStateInfo(0).normalizedTime + .2f, 1);
		float turnAmount = Mathf.Atan2(move.x, move.z);
		animator.SetFloat("Forward", move.normalized.z , 0.12f, Time.deltaTime);
		animator.SetFloat("Turn", turnAmount, 0.12f, Time.deltaTime);

        if (velocity.Equals(Vector3.zero))
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        
		animator.SetBool("Moving", moving);
		//animator.SetBool("OnGround", m_IsGrounded);
	}
}
