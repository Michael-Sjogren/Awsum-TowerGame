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

    private SingleTowerPlacementArea currentTile;

    void SetTileOutLineColor(SingleTowerPlacementArea tile)
    {
        if(tile != null)
        {
            if(!tile.IsOccupied())
            {
                Outline outlineSystem = tile.GetComponentInChildren<Outline>();
                outlineSystem.enabled = true;
                if (buildMenu.IsPlayerInRange(tile.transform.position))
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

        var tile = GetTileAtRayHit();
        if (tile != null)
        {
            if(currentTile != null && tile != currentTile)
            {
                currentTile.GetComponentInChildren<Outline>().enabled = false;
            }
            currentTile = tile;
            SetTileOutLineColor(currentTile);
        }
        else
        {
            if (currentTile != null)
            {
                currentTile.GetComponentInChildren<Outline>().enabled = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && !IsOverUI())
        {
            SingleTowerPlacementArea newTile = GetTileAtRayHit();
            if(newTile != null)
            {
                if(!newTile.IsOccupied())
                {
                    if(buildMenu.IsPlayerInRange(newTile.transform.position))
                    {
                        buildMenu.SetTargetPlacementArea(newTile);    
                    }
                }
            }
            else
            {
                buildMenu.HideBuildMenu();
            }
        }
	}

    SingleTowerPlacementArea GetTileAtRayHit()
    {
        SingleTowerPlacementArea tile = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray , out hitInfo , 500f , placeableLayer ) && !IsOverUI())
        {
            tile = hitInfo.transform.GetComponent<SingleTowerPlacementArea>();
        }
        return tile;
    }

    private bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
