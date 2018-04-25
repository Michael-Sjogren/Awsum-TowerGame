
using UnityEngine;
using UserInput;

public class PlayerController : MonoBehaviour 
{
	public float playerSpeed;
	public float accel;
	public float jumpForce;
	public LayerMask floorMask;
	[Range(.01f , 3f)]
	public float gravityScale;
	public Animator animator;

	private float velY;
	private CharacterController controller;
    private Vector3 movementVector;
    private IInputManager input;

	private float lookX;
	private float lookY;

    void Start()
	{
		controller = GetComponent<CharacterController>();
        input = InputManager.Instance;
	
    }
	void Update()
	{
		Turn();
		Move();
	}

	void Move()
	{
		// left / right
        float xAxis = input.GetAxis( InputAction.MainHorizontal);
		// foward
        float zAxis = input.GetAxis( InputAction.MainVertical);

		Vector3 forward =this.transform.forward;
		Vector3 right = this.transform.right;

		if(GameManager.instance.isInBuildMode)
		{
			forward = Camera.main.transform.up;
			movementVector = ( (forward * zAxis) +  (right * xAxis) ).normalized;

		}
		else 
		{
        	movementVector = ( (forward * zAxis) +  (right * xAxis) ).normalized;
		}

		movementVector *= playerSpeed;
		movementVector.y = velY;

		Vector3 currentVel = Vector3.zero;
	
		if(controller.isGrounded) 
		{
		//	animator.SetBool("OnGround", true);
			animator.SetFloat("Forward" , zAxis );
			currentVel = controller.velocity;
            movementVector.y = 0f;
            if (input.GetButtonDown( InputAction.Jump)) 
			{
				movementVector.y = jumpForce;
		//		animator.SetFloat("Jump" , movementVector.y );
			}
		} else {
		//	animator.SetBool("OnGround", false);
		}
	
		movementVector.y += (Physics.gravity.y * gravityScale) * Time.deltaTime;
		velY = movementVector.y;
		controller.Move(movementVector * Time.deltaTime);
	}

	void Turn()
    {

		lookX += input.GetAxis( InputAction.SubHorizontal);	
		//lookY += input.GetAxis(0 , InputAction.SubVertical);
		//animator.SetFloat("Turn" , controller.velocity.x * Time.deltaTime);
		
		this.transform.localRotation = Quaternion.Euler(0f , lookX  , 0f);
    }
}
