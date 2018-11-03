

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
        agent.SetDestination(worldPoint);
    }

    private bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

