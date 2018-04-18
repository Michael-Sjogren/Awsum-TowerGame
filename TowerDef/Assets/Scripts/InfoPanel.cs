using UnityEngine;
using TMPro;
using System;
public class InfoPanel : MonoBehaviour
{
	public TextMeshProUGUI fireRate; 
	public TextMeshProUGUI buyCost; 
	public TextMeshProUGUI damage;
	public TextMeshProUGUI range;  
	public TextMeshProUGUI unitName;
    public void SetData(TowerData data)
    {
        if(data == null) return;
        if(unitName != null)
		    unitName.SetText(data.name);

        if(fireRate != null)
            fireRate.SetText(data.fireRate.ToString("N2"));
        if(damage != null)
        //    damage.SetText(data.damage.ToString("N1"));
        if(range != null)
            range.SetText(data.range.ToString("N1"));
        if(buyCost != null)
            buyCost.SetText(data.buyCost.ToString());
    }

    public void ClearInfo()
    {   
        if(unitName != null)
       	    unitName.SetText("");
           
        if(fireRate != null)
            fireRate.SetText("");
        if(damage != null)
            damage.SetText("");
        range.SetText("");
        if(buyCost != null)
            buyCost.SetText("");
    }
}
