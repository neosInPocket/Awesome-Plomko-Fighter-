using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusBar : MonoBehaviour
{
	[SerializeField] private float fillSpeed = 0.01f;
	[SerializeField] Image fill;
	[SerializeField] Image placeHolder;
	private float fillValue => fill.fillAmount;
	
	public void RefreshFill(float fillValue)
	{
		StopAllCoroutines();
		StartCoroutine(DoFill(fillValue));
	}
	
	private IEnumerator DoFill(float value)
	{
		placeHolder.fillAmount = value;
		
		while (fillValue != value)
		{
			if (fillValue + fillSpeed > value)
			{
				fill.fillAmount = value;
				yield break;
			}
			
			fill.fillAmount += fillSpeed;
			yield return new WaitForFixedUpdate();
		}
	}
}
