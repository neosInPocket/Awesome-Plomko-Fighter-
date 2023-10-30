using UnityEngine;

public class GameControlling : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	
	private void Start()
	{
		player.Initialize();
		player.Subscribe(PlayerDamageHandler);
	}
	
	private void PlayerDamageHandler(bool isPlayerDead)
	{
		Debug.Log("Player damage taken, is he dead?" + isPlayerDead);
	}
}
