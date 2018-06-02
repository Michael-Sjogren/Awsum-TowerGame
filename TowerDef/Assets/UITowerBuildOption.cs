
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerBuildOption : MonoBehaviour
{
    public GameObject towerPrefab;

    [SerializeField]
    private Button button;
    [SerializeField]
    private TowerData data;
    [SerializeField]
    private TextMeshProUGUI buyPriceText;
    [SerializeField]
    private UITowerBuildMenu buildMenu;
    private Player player;

	void Start ()
    {
        player = PlayerManager.Instance.player;
        buyPriceText.SetText(data.buyCost.ToString());

    }
	
	// Update is called once per frame
	void Update ()
    {
        if ( !player.CanAfford(GetBuyCost()) && button.interactable || data.name == "Earth Tower" )
        {
            button.interactable = false;
        }
        else if( player.CanAfford(GetBuyCost()) && !button.interactable )
        {
            button.interactable = true;
        }
	}

    public void BuyTower()
    {
        buildMenu.BuyTower(data, towerPrefab);
    }

    public float GetFireRate()
    {
        return data.FireRate;
    }

    public float GetDamage()
    {
        return data.Damage;
    }

    public int GetBuyCost()
    {
        return data.buyCost;
    }

    public float GetRange()
    {
        return data.Range;
    }
}
