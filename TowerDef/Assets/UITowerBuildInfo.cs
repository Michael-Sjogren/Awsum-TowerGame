
using TMPro;
using UnityEngine;

public class UITowerBuildInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI towerName;
    [SerializeField]
    private TextMeshProUGUI towerDescription;
    [SerializeField]
    private TextMeshProUGUI towerDamage;
    [SerializeField]
    private TextMeshProUGUI towerFireRate;
    [SerializeField]
    private TextMeshProUGUI towerRange;
    [SerializeField]
    private GameObject towerDescriptionContainer;

    public void ShowTowerDescription(TowerData data)
    {
        towerName.SetText(data.name);
        towerDescription.SetText(data.towerDescription);
        towerDamage.SetText(data.Damage.ToString());
        towerFireRate.SetText(data.FireRate.ToString());
        towerRange.SetText(data.Range.ToString());
        towerDescriptionContainer.SetActive(true);
        // display also radius visualy with selection circle
    }

    public void HideTowerDescription()
    {
        towerDescriptionContainer.SetActive(false);
    }
}
