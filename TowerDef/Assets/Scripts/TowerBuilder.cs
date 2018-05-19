using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UserInput;
using Buildings;
using System;
using TowerDefense.Buildings.Placement;
using JetBrains.Annotations;

public class TowerBuilder : Singleton<TowerBuilder>
{
    // tower witch is shown during placing
    private GameObject _visualTower;
    private RaycastHit raycast;

    /// <summary>
    /// The currently selected tower
    /// </summary>
    public LayerMask placementAreaMask;

    /// <summary>
    /// The layer for tower selection
    /// </summary>
    public LayerMask towerSelectionLayer;

    /// <summary>
    /// The physics layer for moving the ghost around the world
    /// when the placement is not valid
    /// </summary>
    public LayerMask ghostWorldPlacementMask;

    /// <summary>
    /// The radius of the sphere cast 
    /// for checking ghost placement
    /// </summary>
    public float sphereCastRadius = 1;

    private IInputManager input;

    private bool isPlacingTower = false;
    Vector2Int m_GridPosition;
    IPlacementArea m_CurrentArea;
    public bool canPlaceTower { get; private set; }

    // tower prefabs
    public GameObject _fireTowerPrefab;
    public GameObject _frostTowerPrefab;
    public GameObject _earthTowerPrefab;
    public GameObject _waterTowerPrefab;
    public GameObject _lightningTowerPrefab;

    void Start()
    {
        input = UserInput.InputManager.Instance;
    }

    /// <summary>
    /// Places a tower where the ghost tower is
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Throws exception if not in Build State or <see cref="m_CurrentTower"/> is not at a valid position
    /// </exception>
    public void PlaceTower()
    {

        if (!IsGhostAtValidPosition())
        {
            throw new InvalidOperationException("Trying to place tower on an invalid area");
        }

        if (m_CurrentArea == null)
        {
            return;
        }

        if(IsValidPurchase())
        {
            
            Tower createdTower = _visualTower.GetComponent<Tower>();
            createdTower.enabled = false;
            createdTower.Initialize(m_CurrentArea, m_GridPosition);
        }
        else 
        {
            Debug.Log("Not enough money");
        }
        
        isPlacingTower = false;
        _visualTower = null;


        //CancelGhostPlacement();
    }

    /// <summary>
    /// Raycast onto tower placement areas
    /// </summary>
    /// <param name="pointer">The pointer we're testing</param>
    protected void PlacementAreaRaycast()
    {

        // Raycast onto placement area layer
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, placementAreaMask))
        {
            raycast = hit;
        }
    }



    protected void MoveGhost()
    {
        if (_visualTower == null)
        {
            throw new InvalidOperationException(
                "Trying to position a tower ghost while the UI is not currently in the building state.");
        }

        // Raycast onto placement layer
        PlacementAreaRaycast();

        if (raycast.collider != null)
        {
            MoveGhostWithRaycastHit(raycast);
        }
    }

    /// <summary>
    /// Move ghost with successful raycastHit onto m_PlacementAreaMask
    /// </summary>
    protected virtual void MoveGhostWithRaycastHit(RaycastHit raycast)
    {
        // We successfully hit one of our placement areas
        // Try and get a placement area on the object we hit
        m_CurrentArea = raycast.collider.GetComponent<IPlacementArea>();
        Tower ghostTower = _visualTower.GetComponent<Tower>();
        if (m_CurrentArea == null)
        {
            Debug.LogError("There is not an IPlacementArea attached to the collider found on the m_PlacementAreaMask");

        }
        m_GridPosition = m_CurrentArea.WorldToGrid(raycast.point, ghostTower.dimensions);
        TowerFitStatus fits = m_CurrentArea.Fits(m_GridPosition, ghostTower.dimensions);

        //ghostTower.Show();
        ghostTower.transform.position = m_CurrentArea.GridToWorld(m_GridPosition, Vector2Int.one);
    }

    private bool IsValidPurchase()
    {
        Player player = PlayerManager.Instance.player;
        Tower tower = _visualTower.GetComponent<Tower>();
        int buyCost = tower.buyCost;
        return player.playerMoney >= buyCost;
    }

    public bool IsGhostAtValidPosition()
    {
        if (_visualTower == null)
        {
            return false;
        }
        if (m_CurrentArea == null)
        {
            return false;
        }
        Tower ghostTower = _visualTower.GetComponent<Tower>();
    
        TowerFitStatus fits = m_CurrentArea.Fits(m_GridPosition, ghostTower.dimensions );
        return fits == TowerFitStatus.Fits;
    }

    public void StartTowerPlacing(MenuOption option)
    {
        if (!isPlacingTower)
        {
            GameObject towerPrefab = null;
            switch (option)
            {
                case MenuOption.FIRE:
                    towerPrefab = _fireTowerPrefab;
                    break;
                case MenuOption.EARTH:
                    towerPrefab = _earthTowerPrefab;
                    break;
                case MenuOption.WATER:
                    towerPrefab = _waterTowerPrefab;
                    break;
                case MenuOption.LIGHTING:
                    towerPrefab = _lightningTowerPrefab;
                    break;
                case MenuOption.FROST:
                    towerPrefab = _frostTowerPrefab;
                    break;
                default:
                    return;
            }
     
            isPlacingTower = true;
            Vector3 pos = PlayerManager.Instance.player.GetPlayerCursor();
            GameObject towerObj = (Instantiate(towerPrefab , pos, Quaternion.identity) as GameObject);
            Tower tower = towerObj.GetComponent<Tower>();
            int x = (int)towerObj.GetComponent<Collider>().bounds.size.x;
            int z = (int)towerObj.GetComponent<Collider>().bounds.size.z;
            tower.enabled = false;
            towerObj.GetComponent<Tower>().dimensions = new Vector2Int(x , z);
            _visualTower = tower.gameObject;
        //    UnitSelectionSystem.instance.SetFocus(tower.gameObject);
        }
    }

    void Update()
    {
        if (isPlacingTower && _visualTower != null)
        {
            MoveGhost();
            if (input.GetButtonDown( InputAction.Submit) || input.GetButtonDown(InputAction.Interact))
            {
                if (IsGhostAtValidPosition())
                {
                    PlaceTower();
                    GameManager.instance.EnableCameraRotation();
                    GameManager.instance.isInBuildMode = false;
                }

            }
        }

        if (input.GetButtonDown( InputAction.Jump) ||
        input.GetButtonDown( InputAction.WheelMenu) ||
        input.GetButtonDown( InputAction.Cancel)
        )
        {
            isPlacingTower = false;
        //    UnitSelectionSystem.instance.UnFocus();
            Destroy(_visualTower);
            GameManager.instance.EnableCameraRotation();
            GameManager.instance.isInBuildMode = false;
            return;
        }

    }
}


