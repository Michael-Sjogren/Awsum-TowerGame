﻿using Buildings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Buildings.Placement;
using UnityEngine;

public class UITowerBuildMenu : MonoBehaviour
{
    private SingleTowerPlacementArea placementArea;
    [SerializeField]
    private GameObject container;

    private Player player;

    public void Start()
    {
        player = PlayerManager.Instance.player;
        container.SetActive(false);
    }

    public void SetTargetPlacementArea(SingleTowerPlacementArea placementArea)
    {
        this.placementArea = placementArea;
        container.SetActive(true);
    }

    public void HideBuildMenu()
    {
        container.SetActive(false);
    }

    public void BuyTower(TowerData data , GameObject towerPrefab )
    {
        if (player.CanAfford(data.buyCost))
        {
            player.BuyItem(data.buyCost);
            int x = (int)placementArea.transform.position.x;
            int z = (int)placementArea.transform.position.z;
            Vector2Int destination = new Vector2Int(x ,z);
            GameObject towerObj = (Instantiate(towerPrefab, placementArea.transform.position ,  Quaternion.identity) as GameObject);
            Tower tower = towerObj.GetComponent<Tower>();
            int width = (int)towerObj.GetComponent<Collider>().bounds.size.x;
            int depth = (int)towerObj.GetComponent<Collider>().bounds.size.z;
            towerObj.GetComponent<Tower>().dimensions = new Vector2Int(width, depth);
            tower.enabled = false;
            tower.Initialize(placementArea, destination);
            HideBuildMenu();
        }
    }

    public void Update()
    {
        if(placementArea != null)
        {
            Vector2 targetScreenPos = Camera.main.WorldToScreenPoint(placementArea.transform.position);
            transform.position = targetScreenPos;
        }
    }

    public bool IsPlayerInRange(Vector3 pos)
    {
        float distance = Vector3.Distance(player.transform.position, pos);
        return distance <= player.buildRange;
    }
}