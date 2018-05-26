using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

public abstract class GUITowerPerkPanel : GUITowerSelectPanel 
{
    /*
	public GUITowerPerkLevel perkOptionGuiPrefab;
	private List<GUITowerPerkLevel> perks = new List<GUITowerPerkLevel>(5);
	private Tower currentTower;
    public override void OnTowerSelected(Tower tower)
    {
		currentTower = tower;
		if(tower != null) 
		{
			int length = currentTower.perkTreeData.perkLevels.Length;
			for (int i = length -1; i >= 0 ; i--)
			{
				PerkLevel perk = currentTower.perkTreeData.perkLevels[i];
				if(perk != null )
				{
					int index = (length -1) - i;
					GUITowerPerkLevel p = perks[index];
					p.gameObject.SetActive(true);
					p.requierdTowerLevel = perk.unlockLevel;
					p.UpdatePerk(tower);
				}
			}
		}

    }


    // Use this for initialization
    void Start () {

		int length = 5;
		for (int i = 0; i < length; i++)
		{
			GameObject p = Instantiate(perkOptionGuiPrefab.gameObject , this.transform ) as GameObject;
			p.transform.localPosition = Vector3.zero;
			GUITowerPerkLevel pl = p.GetComponent<GUITowerPerkLevel>();
			Color orginalDisabledColor = pl.buttonA.colors.disabledColor;
			pl.orginalDisabledColor = orginalDisabledColor;
			pl.perkIndex = i;
			pl.UpdatePerk(null);	
			perks.Add(pl);
			p.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(currentTower != null && perks.Count > 0) 
		{
			foreach(GUITowerPerkLevel p in perks)
			{
				p.UpdatePerk(currentTower);
			}
		}
	}
    */
}
