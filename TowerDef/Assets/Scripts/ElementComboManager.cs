using System.Collections;
using System.Collections.Generic;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;

public class ElementComboManager : Singleton<ElementComboManager> 
{
	[SerializeField]
	private ElementCombineableData elementComboData;

	public StatusEffectData CanAbsorbEffect( StatusEffectSystem system ,  ElementType elementType) 
	{
		// get list of opposites for this elementType
		OppositeGroup[] opposites = null;
		foreach(ElementGroup group in elementComboData.elementData) 
		{
			if(group.element == elementType) 
			{
				opposites = group.opposites;
				break;
			}
		}

		if(opposites == null) return null;
		// loop over current effects on enemy
		for (int i = system.StatusEffects.Count - 1; i >= 0 ; i--)
		{
			StatusEffect effect = system.StatusEffects[i];
			// check if any of the oppsites match the current effects element
			for(int j = 0; j < opposites.Length; j++) 
			{
				// if it matches absorb and return resultingEffect
				if(effect.elementType == opposites[j].oppositeElement) 
				{
					return opposites[j].resultingEffect;
				}
			}
		}

		return null;
	}	
}
