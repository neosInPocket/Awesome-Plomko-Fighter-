using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
	[SerializeField] private GameControlling gameControlling;
	[SerializeField] private GameStatusController statusController;
	[SerializeField] private UIGameResult uIGameResult;
	
	public void PlayerDamageHandler(bool isPlayerDead)
	{
		
	}
	
	public void PlayerEnemyKilledHandler(int coinsToAdd)
	{
		if (gameControlling.CurrentProgress + coinsToAdd > gameControlling.Reward)
		{
			uIGameResult.Appear(false, gameControlling.Reward);
		}
		
		statusController.RefreshStatus(gameControlling.CurrentProgress / gameControlling.Reward);
	}
}
