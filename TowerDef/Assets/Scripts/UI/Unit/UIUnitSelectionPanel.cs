
using TMPro;
using UnityEngine;

public class UIUnitSelectionPanel : UIUnitSelection
{
    public GameObject mainPanel;
    public TextMeshProUGUI unitNameText;
    public void Awake()
    {
        mainPanel.SetActive(false);
    }
    public override void UpdateUI(LivingEntity entity)
    {
        if(entity == null) 
        {
            mainPanel.SetActive(false);
            return;
        }
        else 
        {
            unitNameText.SetText(entity.name);
            mainPanel.SetActive(true);
        }
    }
}