using Unity.Collections;
using UnityEngine;

public class GameControlling : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	[SerializeField] private GameUIController gameUIController;
	private int level;
	private float reward;
	private float currentProgress = 0;
	private GamePreferences gamePreferences;
	public float CurrentProgress => currentProgress;
	public float Reward => reward;
	
	private void Start()
	{
		gamePreferences = new GamePreferences();
	}
	
	public void RestartGame()
	{
		level = gamePreferences.PlayerLevelSave;
		reward = 2 * Mathf.Log(level * level) + 2;
		currentProgress = 0;
		
		player.Initialize();
		player.SubscribeDamageEvent(PlayerDamageHandler);
		player.SubscribeEnemyKillEvent(PlayerEnemyKilledHandler);
		player.SubscribeDamageEvent(gameUIController.PlayerDamageHandler);
		player.SubscribeEnemyKillEvent(gameUIController.PlayerEnemyKilledHandler);
	}
	
	private void PlayerDamageHandler(bool isPlayerDead)
	{
		if (isPlayerDead)
		{
			UnsubscribePlayer();
		}
	}
	
	private void PlayerEnemyKilledHandler(int coinsToAdd)
	{
		if (currentProgress + coinsToAdd > reward)
		{
			UnsubscribePlayer();
		}
		else
		{
			currentProgress += coinsToAdd;
		}
	}
	
	private void OnDestroy()
	{
		UnsubscribePlayer();
	}
	
	private void UnsubscribePlayer()
	{
		player.UnSubscribeDamageEvent(PlayerDamageHandler);
		player.UnSubscribeEnemyKillEvent(PlayerEnemyKilledHandler);
		player.UnSubscribeDamageEvent(gameUIController.PlayerDamageHandler);
		player.UnSubscribeEnemyKillEvent(gameUIController.PlayerEnemyKilledHandler);
	}
}
