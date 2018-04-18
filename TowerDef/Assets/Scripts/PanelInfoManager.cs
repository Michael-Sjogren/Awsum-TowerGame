
using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoManager : MonoBehaviour {

	public GameObject buyPanel;
	private PanelFader panelFader;
	public GameObject selectionPanel;
	// Use this for initialization
	public void Start()
	{
		panelFader = GetComponent<PanelFader>();
	}
	public void ShowUnitSelectionPanel(TowerData data)
	{
		panelFader.FadeIn();
		selectionPanel.gameObject.SetActive(true);
		selectionPanel.GetComponent<InfoPanel>().SetData(data);
	}

	public void ShowBuyTowerSelectionPanel(TowerData data)
	{
		panelFader.FadeIn();
		buyPanel.gameObject.SetActive(true);
		buyPanel.GetComponent<InfoPanel>().SetData(data);
	}

	public void HideBuyPanel()
	{
		panelFader.FadeOut();
		buyPanel.gameObject.SetActive(false);
	}

	public void HideSelectionPanel()
	{
		panelFader.FadeOut();
		selectionPanel.gameObject.SetActive(false);
	}
}
