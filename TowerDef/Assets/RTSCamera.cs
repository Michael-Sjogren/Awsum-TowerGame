using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInput;

public class RTSCamera : MonoBehaviour {

	private IInputManager input;
	public Vector3 cameraPosOffset;
	public float speed = 5f;
	[Range(0f , 1f)]
	public float cameraSmoothing = 2f;
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
