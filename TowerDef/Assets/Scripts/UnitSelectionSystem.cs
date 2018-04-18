
using UnityEngine;
using Buildings;

public class UnitSelectionSystem : Singleton<UnitSelectionSystem>
{
    [HideInInspector]
    public GameObject unitFocused = null;
    public PanelInfoManager panelInfoManager;
    public void Update()
    {
        if(UnityEngine.Input.GetMouseButtonDown(1) && unitFocused != null) 
		{
			UnFocus();
		}
    }
    public void SetFocus(GameObject o)
    {
        if(o == null ) return;
 		if(unitFocused != null) 
            UnFocus();
        this.unitFocused = o;
        displayUnitInfo();
        DrawCircleAroundUnit(o);
    }

    private void displayUnitInfo()
    {   
        Tower t = unitFocused.GetComponent<Tower>();
        if(t != null)
            panelInfoManager.ShowUnitSelectionPanel(t.data);
    }

    private void hideUnitInfo()
    {   
        panelInfoManager.HideSelectionPanel();
        panelInfoManager.HideBuyPanel();
    }

    public void UnFocus()
    {
        if(unitFocused == null) return;
        unitFocused.GetComponent<Unit>().isFocused = false;
        RemoveCircleRendererFromUnit(unitFocused);
        hideUnitInfo();
    }

    
	public void UpgradeTower() 
    {
        Debug.Log("Upgrade tower");
	}

	public void SellTower()
    {
		Debug.Log("Sell tower");
        Bounds bounds = unitFocused.GetComponent<Collider>().bounds;
        unitFocused.transform.Translate(new Vector3( 0 , -100f  , 0));
        Tower t = unitFocused.GetComponent<Tower>();
        t.placementArea.Clear(t.gridPosition , t.dimensions);
        UnFocus();
        Destroy(unitFocused);

	}

    public void DrawCircleAroundUnit(GameObject o)
    {
        float radius = 1.25f;
        if(o.GetComponent<CircleRenderer>() == null)
        	o.AddComponent(typeof( CircleRenderer));

        Tower t = o.GetComponent<Tower>();
        if(t != null) 
			radius = t.data.range;

        o.GetComponent<CircleRenderer>().radius = radius;
        o.GetComponent<CircleRenderer>().DoRenderer();
    }

    public void RemoveCircleRendererFromUnit(GameObject o)
    {
        LineRenderer lineRend = unitFocused.GetComponent<LineRenderer>();
        CircleRenderer rend = unitFocused.GetComponent<CircleRenderer>();
        Destroy(rend);
        Destroy(lineRend);
    }
    
}