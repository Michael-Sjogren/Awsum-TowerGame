using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelFader : MonoBehaviour {

	public CanvasGroup canvasGroup;

	public void FadeIn()
	{
		canvasGroup.DOFade( 1f , .35f);
	}

	public void FadeOut()
	{
		canvasGroup.DOFade( 0f , .35f);
	}

}
