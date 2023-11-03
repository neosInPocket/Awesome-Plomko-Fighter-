using UnityEngine;

public class GameUIController : MonoBehaviour
{
	[SerializeField] private GameControlling gameControlling;
	[SerializeField] private GameStatusController statusController;
	[SerializeField] private UIGameResult uIGameResult;
	
	
	public void PlayerDamageHandler(int currentLifes)
	{
		statusController.RefreshLifes(currentLifes);
	}
	
	public void PlayerEnemyKilledHandler(float fillValue)
	{
		if (fillValue == 1f)
		{
			
		}
		
		statusController.RefreshStatus(fillValue);
	}
	
	public void SetLevelText(int level)
	{
		statusController.SetLevel(level);
	}
	
	public void AppearGameResultScreen(bool isLose)
	{
		if (!isLose)
		{
			uIGameResult.Appear(false, gameControlling.LevelReward);
		}
		else
		{
			uIGameResult.Appear(true);
		}
	}
}
