using Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuiTowerSelectDescription : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private TextMeshProUGUI damageText;
    [SerializeField]
    private TextMeshProUGUI fireRateText;
    [SerializeField]
    private TextMeshProUGUI rangeText;
    [Header("Colors for positive and negative stat changes")]
    [SerializeField]
    private Color positiveStatChange;
    [SerializeField]
    private Color negativeStatChange;
    [Header("Containers for all the sats shown")]
    [SerializeField]
    private GameObject statsContainer;
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GUITowerSelectSystem towerSelectSystem;
    private bool isInfoDisplayed = false;

    public void ShowSellInfo()
    {
        int sellPrice = towerSelectSystem.GetSelectedTower().sellPrice;
        title.SetText("Sell Tower");
        descriptionText.SetText("You will recive<#FF8000><b> " + sellPrice + "</b> gold<#FFFFFF> for selling this tower.");
        HideStats();
    }

    public void ShowUpgradeInfo()
    {
        Tower tower = towerSelectSystem.GetSelectedTower();
        title.SetText("Upgrade Tower");
        descriptionText.SetText("Upgrading this tower will increase it's level, enabling it to gain access to perks and better stats.");
        UpdateUpgradePreviewStats();
        ShowStats();
    }

    public void ToggleTowerInfo()
    {
        Tower tower = towerSelectSystem.GetSelectedTower();
        TowerData data = tower.data as TowerData;

        title.SetText(data.name);
        descriptionText.SetText(data.towerDescription);
        UpdateStats();
        ShowStats();
    }

    public void HidePanel()
    {
        towerSelectSystem.GetSelectedTower().UpdateSelectionCircleRadius();
        towerSelectSystem.GetSelectedTower().OnStatChanged -= UpdateStats;
        container.SetActive(false);
    }

    private void UpdateStats()
    {
        var tower = towerSelectSystem.GetSelectedTower();
        damageText.SetText(tower.Damage.Value.ToString());
        fireRateText.SetText(tower.FireRate.Value.ToString());
        rangeText.SetText(tower.Range.Value.ToString());
    }

    private void UpdateUpgradePreviewStats()
    {
        var tower = towerSelectSystem.GetSelectedTower();
        var upgradeData = tower.GetUpgradeData();

        Stat damage = new Stat();
        Stat fireRate = new Stat();
        Stat range = new Stat();

        damage.BaseValue = tower.Damage.Value;
        fireRate.BaseValue = tower.FireRate.Value;
        range.BaseValue = tower.Range.Value;

        float dmg = damage.Value;
        float rate = fireRate.Value;
        float rng = range.Value;

        damage.AddModifer(upgradeData.DamageIncrease);
        range.AddModifer(upgradeData.RangeIncrease);
        fireRate.AddModifer(upgradeData.FireRateIncrease);

        float deltaDmg = damage.Value - dmg;
        float deltaFireRate = fireRate.Value - rate;
        float deltaRange = range.Value - rng;

        deltaFireRate = (float)Math.Round((decimal)deltaFireRate, 2);

        string mathOp = GetMathOperatorSymbolBasedOnValue(deltaDmg);
        string hexColor = GetHexColorFromDeltaValue(deltaDmg);

        damageText.SetText(damage.Value.ToString() + " (<#" + hexColor + ">" + mathOp + deltaDmg + "</color>)");

        mathOp = GetMathOperatorSymbolBasedOnValue(deltaFireRate);
        hexColor = GetHexColorFromDeltaValue(deltaFireRate);

        fireRateText.SetText(fireRate.Value.ToString() + " (<#" + hexColor + ">" + mathOp + deltaFireRate + "</color>)");

        mathOp = GetMathOperatorSymbolBasedOnValue(deltaRange);
        hexColor = GetHexColorFromDeltaValue(deltaRange);

        rangeText.SetText(range.Value.ToString() + " (<#" + hexColor + ">" + mathOp + deltaRange + "</color>)");
        tower.UpdateSelectionCircleRadius(range.Value);
    }

    public string GetHexColorFromDeltaValue(float value)
    {
        Color color = (value > 0) ? positiveStatChange : negativeStatChange;
        if (value == 0)
        {
            color = new Color(0, 0, 0, 0);
        }
        return ColorUtility.ToHtmlStringRGBA(color);
    }

    public string GetMathOperatorSymbolBasedOnValue(float value)
    {
        string mathOperator = (value > 0) ? "+" : "-";
        if (value == 0)
        {
            mathOperator = "";
        }
        return mathOperator;
    }

    public void ShowPanel()
    {
        towerSelectSystem.GetSelectedTower().OnStatChanged += UpdateStats;
        container.SetActive(true);
    }

    public void HideStats()
    {
        statsContainer.SetActive(false);

    }

    public void ShowStats()
    {
        statsContainer.SetActive(true);
    }

}
