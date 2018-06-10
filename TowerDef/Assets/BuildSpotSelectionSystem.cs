using System.Collections;
using System.Collections.Generic;
using TowerDefense.Buildings.Placement;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSpotSelectionSystem : MonoBehaviour
{
    [SerializeField]
    private UITowerBuildMenu buildMenu;
    [SerializeField]
    private LayerMask placeableLayer;
    [SerializeField]
    private Color inRangeColor;
    [SerializeField]
    private Color outOfRangeColor;
    [SerializeField]
    private Camera mainCamera;

    private BuildSpot currentBuildSpot;

    void SetTileOutLineColor(BuildSpot spot)
    {
        if(spot != null)
        {
            if(!spot.IsOccupied && !buildMenu.isMenuShowing)
            {
                Outline outlineSystem = spot.GetComponent<Outline>();
                outlineSystem.enabled = true;
                if (buildMenu.IsPlayerInRange(spot.transform.position))
                {
                    outlineSystem.OutlineColor = inRangeColor;
                }
                else
                {
                    outlineSystem.OutlineColor = outOfRangeColor;
                }
            }
        }
    }

    void Update ()
    {

        var spot = GetBuildSpotFromRay();
        if (spot != null)
        {
            if(currentBuildSpot != null && spot != currentBuildSpot)
            {
                currentBuildSpot.GetComponentInChildren<Outline>().enabled = false;
            }
            currentBuildSpot = spot;
            SetTileOutLineColor(currentBuildSpot);
        }
        else
        {
            if (currentBuildSpot != null)
            {
                currentBuildSpot.GetComponentInChildren<Outline>().enabled = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && !IsOverUI())
        {
            BuildSpot newSpot = GetBuildSpotFromRay();
            if(newSpot != null)
            {
                if(!newSpot.IsOccupied)
                {
                    if(buildMenu.IsPlayerInRange(newSpot.transform.position))
                    {
                        buildMenu.SetTargetBuildSpot(newSpot);    
                    }
                }
            }
            else
            {
                buildMenu.HideBuildMenu();
            }
        }
	}

    BuildSpot GetBuildSpotFromRay()
    {
        BuildSpot spot = null;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.SphereCast(ray.origin , .5f , ray.direction , out hitInfo , 500f , placeableLayer , QueryTriggerInteraction.Collide ) && !IsOverUI())
        {
            spot = hitInfo.transform.GetComponent<BuildSpot>();
        }
        return spot;
    }

    private bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
