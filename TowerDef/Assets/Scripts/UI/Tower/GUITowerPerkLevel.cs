using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUITowerPerkLevel : MonoBehaviour
{
	[HideInInspector]
	public int requierdTowerLevel = 0;
	[HideInInspector]
	public int perkIndex = 0;
	[HideInInspector]
	public TextMeshProUGUI towerLevelText;
	public TextMeshProUGUI optionAText;
	public TextMeshProUGUI optionBText;
	public Button buttonA;
	public Button buttonB;
    private Tower currentTower;
	public Color buttonPickedDisabledColor;
	public Color orginalDisabledColor;

	void Start()
	{
		
		UpdatePerk(null);
	}
    public void PickPerk(Button button) 
	{
		if(currentTower == null) return;
		Debug.Log("Picked perk");
		PerkLevel perkLevel = currentTower.perkTreeData.perkLevels[perkIndex];
		PerkOption perkPicked = default(PerkOption); 
		if(button == buttonA) 
		{
			perkPicked = perkLevel.perkA;
		}
		else if(button == buttonB)
		{
			perkPicked = perkLevel.perkB;
		}
		PerkOption p; 
		currentTower.perksSelected.TryGetValue(requierdTowerLevel , out p);
		if( p.Equals(default(PerkOption)) ) 
		{
        	currentTower.perksSelected.Add(requierdTowerLevel , perkPicked );
		}else
		{
			currentTower.perksSelected[requierdTowerLevel] = perkPicked;
		}

		currentTower.AddPerk(perkPicked);
		ApplySelectedDisabledColor(button);
		DisableButtons();
	}

    public void UpdatePerk(Tower tower)
    {
		currentTower = tower;
		if(currentTower != null) 
		{
			if(currentTower.perkTreeData == null) currentTower.Initialize();
			if(currentTower.perkTreeData.perkLevels == null) return;
			if(currentTower.perkTreeData.perkLevels.Length <= 0) return;
			if(perkIndex >= currentTower.perkTreeData.perkLevels.Length) return;
			
			PerkLevel perkLevel = currentTower.perkTreeData.perkLevels[perkIndex];
        	towerLevelText.SetText(requierdTowerLevel.ToString());
        	optionAText.SetText(perkLevel.perkA.description.ToString());
        	optionBText.SetText(perkLevel.perkB.description.ToString());
			
			int currentLevel = tower.Level;
			// check if current level is above or equal to the required level : else disable buttons and reset disabled color for both
			if(currentLevel >= requierdTowerLevel) 
			{
				// check if this perk level has been selcted
				bool isPerkChosen = currentTower.perksSelected.ContainsKey(requierdTowerLevel);
				if(isPerkChosen) 
				{
					// check what perk is selected and apply selected color to the chosen one
					PerkOption p = currentTower.perksSelected[requierdTowerLevel];
					Button button = null;
					if(p.Equals(perkLevel.perkA)) 
					{
						button = buttonA;
						ResetSelectedDisabledColor(buttonB);
					}
					else if(p.Equals(perkLevel.perkB))
					{
						button = buttonB;
						ResetSelectedDisabledColor(buttonA);
					}else {
						Debug.Log("Selected perk does not have a valid perkoption selected");
						return;
					}
					ApplySelectedDisabledColor(button);
					DisableButtons();
				}
				else 
				{
					EnableButtons();
				}
			}
			else 
			{
				ResetButtonsDisabledColor();
				DisableButtons();
			}
		}	
    }

    private void ResetButtonsDisabledColor()
    {
        ColorBlock cb = buttonA.colors;
		cb.disabledColor = orginalDisabledColor;
		buttonA.colors = cb;
		buttonB.colors = cb;
    }

    public void ResetSelectedDisabledColor(Button b)
	{
		ColorBlock cb = b.colors;
		cb.disabledColor = orginalDisabledColor;
		b.colors = cb;
	}

	public void ApplySelectedDisabledColor(Button b)
	{
		ColorBlock cb = b.colors;
		cb.disabledColor = buttonPickedDisabledColor;
		b.colors = cb;
	}

	public void EnableButtons()
	{
		buttonA.interactable = true;
		buttonB.interactable = true;
	}

	public void DisableButtons()
	{
		buttonA.interactable = false;
		buttonB.interactable = false;
	}

	public void DisableButton(Button b)
	{
		b.interactable = false;
	}	
}
