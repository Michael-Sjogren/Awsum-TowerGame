
using Buildings;
using TMPro;

public class GUITowerSelectTowerInfo : GUITowerSelectPanel
{
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI sellPriceText;
    public override void OnTowerSelected(Tower tower)
    {
        if(tower == null) return;
        buildingName.SetText(tower.data.name);
        // todo
        damageText.SetText("?");
        
        fireRateText.SetText(tower.fireRate.ToString());
        rangeText.SetText(tower.range.ToString());
        sellPriceText.SetText(tower.sellPrice.ToString());
    }
}