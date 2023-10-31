using UnityEngine;

public class GameControlling : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	
	private void Start()
	{
		player.Initialize();
		player.SubscribeDamageEvent(PlayerDamageHandler);
		player.SubscribeEnemyKillEvent(PlayerEnemyKilledHandler);
	}
	
	private void PlayerDamageHandler(bool isPlayerDead)
	{
		Debug.Log("Player damage taken, is he dead?" + isPlayerDead);
	}
	
	private void PlayerEnemyKilledHandler(int coinsToAdd)
	{
		
	}
	
	private void OnDestroy()
	{
		UnsubscribePlayer();
	}
	
	private void UnsubscribePlayer()
	{
		player.UnSubscribeDamageEvent(PlayerDamageHandler);
		player.UnSubscribeEnemyKillEvent(PlayerEnemyKilledHandler);
	}
}
