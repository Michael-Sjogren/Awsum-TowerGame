using Buildings;
using UnityEngine;

public class BuildSpot : MonoBehaviour
{

    private bool isOccupied = false;

    public bool IsOccupied
    {
        get { return isOccupied; }
        set
        {
            isOccupied = value;
            buildSpotLight.enabled = !value;
        }
    }

    [SerializeField]
    private Tower currentTower;
    public Transform buildOrigin;
    [SerializeField]
    private Light buildSpotLight;

    void Start()
    {
        if (currentTower != null)
        {
            BuildTower(currentTower.gameObject);
        }
        else
        {
            buildSpotLight.enabled = true;
        }
    }


    public Tower BuildTower(GameObject towerPrefab)
    {
        var obj = Instantiate(towerPrefab, buildOrigin.position, Quaternion.identity);
        var tower = obj.GetComponent<Tower>();
        currentTower = tower;
        isOccupied = true;
        return tower;
    }
}
