
using Buildings;
using TMPro;
using UnityEngine;

public class GUITowerSelectTowerInfo : GUITowerSelectPanel
{
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI towerLevel;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI sellPriceText;
    public Animator upgradePanelAnimator;
    public string animationProtperyName = "Display";
    private bool display = false;
    private Tower currentTower;

    void Start()
    {
        upgradePanelAnimator.SetBool( "Display" , false );
    }
    
    public override void OnTowerSelected(Tower tower)
    {
        currentTower = tower;
        if(currentTower != null) 
        {
            towerLevel.SetText(currentTower.Level.ToString());
            buildingName.SetText(currentTower.data.name);
            damageText.SetText(currentTower.Damage.Value.ToString());
            fireRateText.SetText(currentTower.FireRate.Value.ToString());
            rangeText.SetText(currentTower.Range.Value.ToString());
            sellPriceText.SetText(currentTower.sellPrice.ToString());
        }
        
    }

    public void ToggleUpgradeWindow()
    {
        display = !display;
        upgradePanelAnimator.SetBool( "Display" , display );
    }
}
