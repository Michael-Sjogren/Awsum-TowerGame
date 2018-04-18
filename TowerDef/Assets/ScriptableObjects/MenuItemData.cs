using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="New Radial Menuitem" , menuName="Radial Menuitem")]
public class MenuItemData : ScriptableObject 
{
	[Header("Menu Option Icon")]
    public Sprite icon;
	public Color iconNormalColor;
	public Color iconHighlightColor;
    public new string name;
    public MenuOption menuOption;

}
