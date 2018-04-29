using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUnitSelectionStats : UIUnitSelection
{

	public TextMeshProUGUI damageText;
	public TextMeshProUGUI movementSpeedText;
	public TextMeshProUGUI magicArmorText;
	public TextMeshProUGUI armorText;

    public override void UpdateUI(LivingEntity entity)
    {
		if(entity == null) return;
        movementSpeedText.SetText(entity.MovementSpeed.ToString());
		// TODO 
        damageText.SetText("1");
		// TODO
		magicArmorText.SetText("1");
		// TODO
		armorText.SetText("1");
    }
}
