
using UnityEngine;
using Buildings;
using System;

[RequireComponent(typeof(Camera))]
public class UnitSelectionSystem : MonoBehaviour
{
    [HideInInspector]
    public LivingEntity entitySelected = null;
    // todo
    public Tower buildingSelected = null;
    public LayerMask selectablesLayermask;

    public delegate void UnitEventHandler(LivingEntity unit);
    public delegate void BuildingEventHandler(Tower unit);
    public event UnitEventHandler OnUnitChanged = delegate { };
    public event BuildingEventHandler OnBuildingChanged = delegate { };
    private Camera cam;

    void Start()
    {
        cam = this.GetComponent<Camera>();
    }
    public void Update()
    {
        GetSelection();
    }

    private void GetSelectedBuilding()
    {

    }

    private void GetSelection()
    {
        
        IsDead(entitySelected);
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 500f, selectablesLayermask))
            {
                LivingEntity entity = raycastHit.transform.GetComponent<LivingEntity>();
                Tower tower = raycastHit.transform.GetComponent<Tower>();
                if (tower != null)
                {
                    UpdateTowerSelection(tower);
                }
                else if (entity != null)
                {
                    UpdateUnitSelection(entity);
                }

            }
        }
        else if (UnityEngine.Input.GetMouseButtonDown(1))
        {
            //entitySelected.UpdateSelectionCiricle(false);
            //entitySelected = null;
            //OnUnitChanged(null);
            if(buildingSelected != null)
            {
                OnBuildingChanged(null);
                buildingSelected.UpdateSelectionCiricle(false);
                buildingSelected = null;
            }
        }
    }

    private void UpdateTowerSelection(Tower tower)
    {
        if (buildingSelected != null)
            buildingSelected.UpdateSelectionCiricle(false);

        buildingSelected = tower;
        buildingSelected.UpdateSelectionCiricle(true);
        OnBuildingChanged(tower);
    }

    private void UpdateUnitSelection(LivingEntity entity)
    {
        if (IsDead(entity))
        {
            return;
        }

        if (entitySelected != null)
            entitySelected.UpdateSelectionCiricle(false);

        entitySelected = entity;
        entitySelected.UpdateSelectionCiricle(true);
        OnUnitChanged(entity);
    }

    private bool IsDead(LivingEntity entity)
    {
        if (entity == null) return true;
        if (!entity.IsAlive)
        {
            Debug.Log("selected Unit was dead");
            OnUnitChanged(null);
            return true;
        }
        OnUnitChanged(entity);
        return false;
    }
}