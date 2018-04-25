
using UnityEngine;
using Buildings;

[RequireComponent(typeof(Camera))]
public class UnitSelectionSystem : MonoBehaviour
{
    [HideInInspector]
    public Entity entitySelected = null;
    public LayerMask selectablesLayermask;

    public delegate void UnitEventHandler(IUnit unit);
    public event UnitEventHandler OnUnitChanged = delegate{};
    private Camera cam;

    void Start()
    {
       cam = this.GetComponent<Camera>(); 
    }
    public void Update()
    {
        if(UnityEngine.Input.GetMouseButtonDown(0)) 
		{
			RaycastHit raycastHit;
            Vector2 upperLeft = Input.mousePosition;
           
            Ray ray =  cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray , out raycastHit , 500f , selectablesLayermask , QueryTriggerInteraction.Collide))
            {
                Entity entity = raycastHit.transform.GetComponent<Entity>();
                if(entity == null) 
                {
                    Debug.Log("it hit a selectable but The unit was null!!?");
                    return; 
                }
                if(entity == entitySelected) return;
                
                Debug.Log("Unit hit: " + entity.name );
                if(entitySelected != null)
                    entitySelected.UpdateSelectionCiricle(false);
                    
                entity.UpdateSelectionCiricle(true);
                entitySelected = entity;
                OnUnitChanged(entity);  
            }
        }
        else if(UnityEngine.Input.GetMouseButtonDown(1) && entitySelected != null)
        {
            entitySelected.UpdateSelectionCiricle(false);
            entitySelected = null;
            OnUnitChanged(null);
        }
    }
    public void SetFocus(Entity unit)
    {
    /*
        if(o == null ) return;
 		if(unitFocused != null) 
            UnFocus();
        this.unitFocused = o;
        displayUnitInfo();
        DrawCircleAroundUnit(o);
    */
    }

    private void displayUnitInfo()
    {   
       
        //if(t != null) panelInfoManager.ShowUnitSelectionPanel(t.data);
    }

    private void hideUnitInfo()
    {   
    //    panelInfoManager.HideSelectionPanel();
    //    panelInfoManager.HideBuyPanel();
    }

    public void UnFocus()
    {
    //    if(unitFocused == null) return;
    //    unitFocused.GetComponent<Unit>().isFocused = false;
    //    RemoveCircleRendererFromUnit(unitFocused);
    //    hideUnitInfo();
    }

    
	public void UpgradeTower() 
    {
    //    Debug.Log("Upgrade tower");
	}

	public void SellTower()
    {
	//	Debug.Log("Sell tower");
    //    Bounds bounds = unitFocused.GetComponent<Collider>().bounds;
    //    unitFocused.transform.Translate(new Vector3( 0 , -100f  , 0));
    //    Tower t = unitFocused.GetComponent<Tower>();
    //    t.placementArea.Clear(t.gridPosition , t.dimensions);
    //    UnFocus();
    //    Destroy(unitFocused);

	}

    public void DrawCircleAroundUnit(GameObject o)
    {
        /* 
        float radius = 1.25f;
        if(o.GetComponent<CircleRenderer>() == null)
        	o.AddComponent(typeof( CircleRenderer));

        Tower t = o.GetComponent<Tower>();
        if(t != null) 
			radius = t.data.range;

        o.GetComponent<CircleRenderer>().radius = radius;
        o.GetComponent<CircleRenderer>().DoRenderer();
        */
    }

    public void RemoveCircleRendererFromUnit(GameObject o)
    {
        /*
        LineRenderer lineRend = unitFocused.GetComponent<LineRenderer>();
        CircleRenderer rend = unitFocused.GetComponent<CircleRenderer>();
        Destroy(rend);
        Destroy(lineRend);
        */
    }
    
}