

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UserInput;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private LivingEntity entity;
    [SerializeField]
    private LayerMask groundLayer;
 
    private void Start()
    {
        GetComponent<UnitSelectionCircle>().EnableCiricle();
        agent = GetComponent<NavMeshAgent>();
        entity = GetComponent<LivingEntity>();
    }

    private void Update()
    {
        if (!IsOverUI())
        {
            if (Input.GetMouseButton(0))
            {
                var ray = GameManager.instance.cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray , out hit , groundLayer ))
                {
                    MoveToMouse(hit.point);
                }
            }
        }
    }

    private void MoveToMouse(Vector3 worldPoint)
    {
        agent.speed = entity.MovementSpeed.Value;
        NavMeshHit navHit;
        bool onMesh = NavMesh.SamplePosition(worldPoint , out navHit , 1 , 1);
        Debug.Log(onMesh);
        if (!IsReachable(transform.position , worldPoint) && !onMesh)
        {
            /*
            NavMeshHit hit;
            bool blocked = NavMesh.Raycast(transform.position, worldPoint, out hit, NavMesh.AllAreas);
            if (blocked)
            {
                Debug.DrawRay(hit.position, Vector3.up, Color.red);
                // move in the direction of the point instead?

                Vector3 direction = new Vector3
                    (
                        transform.position.x - worldPoint.x,
                        0,
                        transform.position.z - worldPoint.z
                    );

                agent.Move(direction.normalized*Time.deltaTime);
            }
            */

        }
        else
        {
            agent.SetDestination(worldPoint);
        }
    }

    private bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsReachable(Vector3 start , Vector3 end)
    {
        bool reachable = false;
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(start , end , NavMesh.AllAreas , path))
        {
            reachable = true;
        }
        return reachable;
    }
}

