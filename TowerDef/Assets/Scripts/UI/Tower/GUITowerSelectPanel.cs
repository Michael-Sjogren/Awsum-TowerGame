using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

public abstract class GUITowerSelectPanel : GUIPanel {

	public abstract void OnTowerSelected(Tower tower);
	// Use this for initialization

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		selectionSystem.OnBuildingChanged += OnTowerSelected;
	}

	void OnDisable()
	{
		selectionSystem.OnBuildingChanged -= OnTowerSelected;
	}
}
