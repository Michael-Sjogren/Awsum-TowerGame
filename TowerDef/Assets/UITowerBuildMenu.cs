
using UnityEngine;

public class UITowerBuildMenu : MonoBehaviour
{
    private BuildSpot currentBuildSpot;
    [SerializeField]
    private GameObject container;
    public bool isMenuShowing;

    private Player player;

    public void Start()
    {
        player = PlayerManager.Instance.player;
        container.SetActive(false);
        isMenuShowing = false;
    }



    public void HideBuildMenu()
    {
        container.SetActive(false);
        isMenuShowing = false;
    }

    public void BuyTower(TowerData data , GameObject towerPrefab )
    {
        if (player.CanAfford(data.buyCost))
        {
            player.BuyItem(data.buyCost);
            var tower = currentBuildSpot.BuildTower(towerPrefab);
            tower.enabled = false;
            tower.InitializeTower(currentBuildSpot);
            HideBuildMenu();
        }
    }

    public void Update()
    {
        if(currentBuildSpot != null)
        {
            Vector3 targetPos = currentBuildSpot.transform.position + Vector3.up * 2.5f;
            transform.position = targetPos;
        }
    }

    public bool IsPlayerInRange(Vector3 pos)
    {
        float distance = Vector3.Distance(player.transform.position, pos);
        return distance <= player.buildRange;
    }

    public void SetTargetBuildSpot(BuildSpot newSpot)
    {
        currentBuildSpot = newSpot;
        container.SetActive(true);
        isMenuShowing = true;
    }
}
