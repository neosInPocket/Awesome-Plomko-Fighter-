using TMPro;
using UnityEngine;

public class UIGameResult : MonoBehaviour
{
	[SerializeField] private TMP_Text restartButtonText;
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text coinsAmount;
	[SerializeField] private GameControlling gameControlling;
	
	public void Appear(bool isLose, int coinsAdded = 0)
	{
		RefreshResultInfo(isLose, coinsAdded);
		gameObject.SetActive(true);
	}
	
	public void Hide()
	{
		gameControlling.RestartGame();
		gameObject.SetActive(false);
	}
	
	private void RefreshResultInfo(bool isLose, int coinsAdded)
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
