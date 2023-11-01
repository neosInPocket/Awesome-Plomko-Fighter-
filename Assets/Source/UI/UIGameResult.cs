using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameResult : MonoBehaviour
{
	[SerializeField] private TMP_Text restartButtonText;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text coinsAmount;
	
	public void Appear(bool isLose, float coinsAdded = 0)
	{
		RefreshResultInfo(isLose);
		gameObject.SetActive(true);
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	
	private void RefreshResultInfo(bool isLose, float coinsAdded = 0)
	{
		if (isLose)
		{
			resultText.text = "LOSE";
			restartButtonText.text = "TRY AGAIN";
		}
		else
		{
			resultText.text = "YOU WIN";
			restartButtonText.text = "NEXT LEVEL";
			coinsAmount.text = coinsAdded.ToString();
		}
	}
}
