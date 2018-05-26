using Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GUITowerSelectSystem : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask towerSelectables;
    [SerializeField]
    private GameObject towerSelectionContainer;

    private Tower tower;
    [SerializeField]
    private TextMeshProUGUI upgradePriceText;
    [SerializeField]
    private Button upgradeButton;

    [SerializeField]
    private GameObject perkOptionContainer;
    [SerializeField]
    private TextMeshProUGUI optionAText;
    [SerializeField]
    private TextMeshProUGUI optionBText;

    private PerkLevel perkLevel;

    public void Update()
    {
        if (tower != null)
        {
            Vector2 targetScreenPos = mainCamera.WorldToScreenPoint(tower.transform.position + tower.transform.up * 4);
            transform.position = targetScreenPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SetTowerFromClickWithRayCast();
        }
    }

    public void ShowTowerSelectionMenu()
    {
        UpdatePerkOptions();
        GetSelectedTower().UpdateSelectionCiricle(true);
        towerSelectionContainer.SetActive(true);
    }

    public void HideTowerSelectionMenu()
    {
        if(tower != null)
        {
            tower.UpdateSelectionCiricle(false);
        }
        towerSelectionContainer.SetActive(false);
    }

    public void UpgradeTower()
    {
        if(IsTowerMaxLevel())
        {
            DisableUpgradeButtonForMaxLevel();
            return;
        }
        tower.Upgrade();
        UpdatePerkOptions();    
        upgradePriceText.SetText(tower.GetUpgradePrice().ToString());
        
    }


    public void SellTower()
    {
        tower.Sell();
        HideTowerSelectionMenu();
    }

    private void SetTowerFromClickWithRayCast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 500f ,  towerSelectables))
        {
            Tower t = hitInfo.transform.GetComponent<Tower>();
            if (t != null)
            {
                if(t.enabled)
                {
                   tower = t;
                   UpdatePerkOptions();
                   ShowTowerSelectionMenu();
                }
            }
        }
        else
        {
            if(!IsOverUI())
            {
                HideTowerSelectionMenu();
            }
        }
    }
    private bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsTowerMaxLevel()
    {
        return tower.maxLevel <= tower.Level;
    }

    public void UpdatePerkOptions()
    {
        if(!tower.hasPickedPerk)
        {
            var level = tower.GetPerkOptionsForCurrentLevel();
            if(level != null)
            {
                optionAText.SetText(level.perkA.description.ToString());
                optionBText.SetText(level.perkB.description.ToString());
                perkLevel = level;
                perkOptionContainer.SetActive(true);
            }
        }
        else if(tower.hasPickedPerk)
        {
            perkOptionContainer.SetActive(false);
        }
        UpdateUpgradeButton();
    }

    private void UpdateUpgradeButton()
    {
        upgradeButton.interactable = false;

        if(tower.hasPickedPerk)
        {
            upgradeButton.interactable = true;
        }

        if(IsTowerMaxLevel())
        {
            DisableUpgradeButtonForMaxLevel();
        }
        else
        {
            upgradePriceText.SetText(tower.GetUpgradePrice().ToString());
        }
    }

    private void DisableUpgradeButtonForMaxLevel()
    {
        upgradePriceText.SetText("MAX");
        upgradeButton.interactable = false;
    }

    public void PickOptionA()
    {
        GetSelectedTower().AddPerk(perkLevel.perkA);
        GetSelectedTower().hasPickedPerk = true;
        UpdatePerkOptions();
    }

    public void PickOptionB()
    {
        GetSelectedTower().AddPerk(perkLevel.perkB);
        GetSelectedTower().hasPickedPerk = true;
        UpdatePerkOptions();
    }

    public Tower GetSelectedTower()
    {
        return tower;
    }
}