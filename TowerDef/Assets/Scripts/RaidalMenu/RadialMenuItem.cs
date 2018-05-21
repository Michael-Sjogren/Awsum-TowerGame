using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RadialMenuItem : MonoBehaviour
{
    public string actionName;
    public Color normalColor;
    public Color highLightColor;
    public Color pressedColor;
    public Image image;
    public Image icon;
    private Color normalIconColor;
    private Color highlightIconColor;
    private MenuOption option;

    void Start()
    {
        
    }
    public void InitializeItem(MenuItemData data)
    {
        SetData(data);
        SetIconPosition();
        SetToNormalColor();
    }

    private void SetIconPosition()
    {
        float fillAmount = image.fillAmount * 360f;
        float degrees = (fillAmount / 2f) - (fillAmount - 90f);  
        float angle = ( degrees ) * Mathf.Deg2Rad;
        float radius = MenuManager.instance.radius;

        float x = Mathf.Cos(angle) * radius - MenuManager.instance.iconOffsetDistance;
        float y = Mathf.Sin(angle) * radius - MenuManager.instance.iconOffsetDistance;

        icon.transform.localPosition = new Vector3( x , y ,0f);
        icon.transform.rotation = Quaternion.identity;
    }
    public void DoAction()
    {
        // do action or go to another nested menu
        MenuManager.instance.PickMenuItem(option);
        SetToNormalColor();
    }

    private /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        icon.transform.rotation = Quaternion.identity;
    }

    private void SetData(MenuItemData data)
    {
        //icon = info.icon;
        icon.sprite = data.icon;
        normalIconColor = data.iconNormalColor;
        highlightIconColor = data.iconHighlightColor;
        option = data.menuOption;
    }
    public void SetAsHighlighted()
    {
        image.color = highLightColor;
        icon.color = highlightIconColor;
    }

    public void SetToNormalColor()
    {
        image.color = normalColor;
        icon.color = normalIconColor;
    }
}
