
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController2 : MonoBehaviour {

    private Rigidbody body;
    private LivingEntity entity;

    [SerializeField]
    private LayerMask ground;
	// Use this for initialization
	void Start ()
    {
        entity = GetComponent<LivingEntity>();
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = GameManager.instance.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray , out hit , ground))
            {
                float y = transform.position.y;
                var dir =  hit.point - transform.position;
                dir.y = 0;
                body.MovePosition(transform.position + dir.normalized * entity.MovementSpeed.Value * Time.deltaTime);
                body.rotation = Quaternion.LookRotation(dir.normalized, Vector3.up);
            }
        }
	}
}
