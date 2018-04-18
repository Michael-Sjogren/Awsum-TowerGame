
using System;
using System.Collections.Generic;
using UnityEngine;
using UserInput;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance { get; private set; }

    public void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}else 
		{
			Destroy(gameObject);
		}
	}

    public GameObject radialMenu;
    public float radius = 0f;
    public float iconOffsetDistance;
	public RadialMenuItem menuItemPrefab;
    // holds data about each radial menu item and what action it contains
    public MenuItemData[] menuItems;
    private RadialMenuItem[] items;
    private IInputManager input;
    private Vector3 originalScale;
    private int currentMenuItem;
    private int prevMenuItem = -1;
    private int itemCount = 0;
    private Dictionary<int , MenuItemData> itemInfoDictionary;

    // Use this for initialization
    void Start()
    {
        input = InputManager.Instance;
        itemCount = menuItems.Length; 
		items = new RadialMenuItem[itemCount];

        radius = radialMenu.GetComponent<RectTransform>().rect.size.x / 2f;	

        HideMenu();

        originalScale = radialMenu.transform.localScale;
        itemInfoDictionary = new Dictionary<int, MenuItemData>(itemCount);
        for (int i = 0; i < itemCount; i++)
        {
            MenuItemData data = menuItems[i];
            MenuOption option = data.menuOption;
            AddMenuItemData(data.menuOption , data);
        }

    }



    private void InstantiateMenuItems()
    {
        for (int i = 0; i < itemCount; i++)
        {
            RadialMenuItem item = Instantiate(menuItemPrefab) as RadialMenuItem;
            item.transform.SetParent(radialMenu.transform , false);
            float fillAmount = (float) ( 360f / itemCount ) / 360f;
            float angle = (360f / itemCount) * i;
            item.image.fillAmount = fillAmount;
            // setup icon image and color 
           
            MenuItemData data = GetMenuItemData(i);
            item.InitializeItem(data);
            item.transform.Rotate(Quaternion.AngleAxis(angle , Vector3.forward ).eulerAngles);
            items[i] = item;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (input.GetButtonDown(0, InputAction.WheelMenu) && !radialMenu.activeSelf)
        {
            ShowMenu();
            GameManager.instance.DisableCameraRotation();
        }

        if(radialMenu.activeSelf) 
        {
            GetCurrentMenuItem();
        }
       
    }

    public void GetCurrentMenuItem()
    {
        // get axis from mouse and joystick
        Vector2 axis = PlayerManager.Instance.player.GetMenuSelectionAxis();
        bool hasTouched = axis != Vector2.zero;
        float angle = Mathf.Atan2 (axis.y, axis.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        // get index of angle
        int i = (int) (angle / (360 / items.Length));
        if(i > items.Length) return;
        if(i != prevMenuItem) 
        {
            currentMenuItem = i;
            RadialMenuItem current = items[i];
            current.SetAsHighlighted();
            if(prevMenuItem != -1) {
                RadialMenuItem prev = items[prevMenuItem];
                prev.SetToNormalColor();
            }
        }

        if(input.GetButtonUp(0 , InputAction.WheelMenu)) 
        {
            RadialMenuItem current = items[i];
            GameManager.instance.EnableCameraRotation();
            current.DoAction();
            HideMenu();
        }
        prevMenuItem = currentMenuItem;
    }

    public void ShowMenu()
    {
		DestroyMenuItems();
		InstantiateMenuItems();
        radialMenu.SetActive(true);
        ScaleMenu(originalScale.x , 0.2f);
        // scale up
    }
    public void PickMenuItem(MenuOption option)
    {
        TowerBuilder.instance.StartTowerPlacing(option); 
    }

    public void HideMenu()
    {
        ScaleMenu(originalScale.x - .5f , 0.3f);
    	radialMenu.SetActive(false);
		DestroyMenuItems();
    }
    public void ScaleMenu( float amount , float time )
    {
        radialMenu.transform.DOScale(amount , time );
    }
    private void DestroyMenuItems()
    {

		for (int i = items.Length - 1; i >= 0 ; i--)
		{
			if(items[i] == null) continue;
			GameObject o = items[i].gameObject;
			Destroy(o);
			items[i] = null;	
		}	
		if( items.Length != itemCount)
			items = new RadialMenuItem[itemCount];
    }

    public MenuItemData GetMenuItemData(int option) 
    {
        MenuItemData itemInfo = itemInfoDictionary[(int)option];
        return itemInfo;
    }

    public void AddMenuItemData(MenuOption option , MenuItemData data ) 
    {
        itemInfoDictionary.Add((int) option , data);
    }
}