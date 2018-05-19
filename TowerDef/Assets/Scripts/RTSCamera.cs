using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInput;

public class RTSCamera : MonoBehaviour {

	private IInputManager input;
	public float speed = 5f;
	public void Start()
	{
		input = InputManager.Instance;
	}
	void LateUpdate()
	{
		Move();
	}

	void Move()
	{
		float xAxis = input.GetAxisRaw( InputAction.MainHorizontal);
		float zAxis = input.GetAxisRaw( InputAction.MainVertical);
		Vector3 pos = this.transform.position;
        Vector3 newPos = new Vector3( pos.x + (xAxis * speed * Time.deltaTime)
									, pos.y 
									, pos.z + (zAxis * speed * Time.deltaTime)
									);
        this.transform.position = newPos;
	}
}
