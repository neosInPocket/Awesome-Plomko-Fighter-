using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameControlling : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	[SerializeField] private GameUIController gameUIController;
	[SerializeField] private PlayerMovementController playerMovementController;
	[SerializeField] private Spawner spawner;
	[SerializeField] private UITutorialScreen uITutorialScreen;
	[SerializeField] private UICountDown uICountDown;
	private int level;
	private int levelReward;
	private int currentProgress = 0;
	private int maxProgress;
	private GamePreferences gamePreferences;
	public int CurrentProgress => currentProgress;
	public int LevelReward => levelReward;
	public int MaxProgress => maxProgress;
	
	private void Start()
	{
		gamePreferences = new GamePreferences();
		RestartGame();
	}
	
	public void RestartGame()
	{
		level = gamePreferences.PlayerLevelSave;
		maxProgress = (int)(5 * Mathf.Log(level * level) + 2);
		levelReward = (int)(7 * Mathf.Log(level * level) + 20);
		currentProgress = 0;
		
		if (gamePreferences.IsTutorialRequired)
		{
			gamePreferences.SetTutorialRequired(false);
			uITutorialScreen.OnTutorialEnded += OnTutorialEndHandler;
		}
		else
		{
			OnTutorialEndHandler();
		}
	}
	
	private void OnTutorialEndHandler()
	{
		uITutorialScreen.OnTutorialEnded -= OnTutorialEndHandler;
		uICountDown.CountDownEnded += OnCountDownEndedHandler;
		uICountDown.Play();
	}
	
	private void OnCountDownEndedHandler()
	{
		uICountDown.CountDownEnded -= OnCountDownEndedHandler;
		InitializePlayer();
		
		gameUIController.PlayerDamageHandler(player.Player.Lifes);
		gameUIController.PlayerEnemyKilledHandler(0);
		gameUIController.SetLevelText(level);
		
		spawner.Enable();
		spawner.Spawn();
	}
	
	private void InitializePlayer()
	{
		player.StopAllCoroutines();
		player.Initialize();
		playerMovementController.EnablePlayerControls();
		player.IsInvincible = false;
		player.transform.position = Vector2.zero;
		player.TrailRenderer.Clear();
		player.Rigid.velocity = Vector2.zero;
		
		player.SubscribeDamageEvent(PlayerDamageHandler);
		player.SubscribeEnemyKillEvent(PlayerEnemyKilledHandler);
	}
	
	private void PlayerDamageHandler(int currentLifes)
	{
		if (currentLifes == 0)
		{
			DisablePlayer();
			gameUIController.AppearGameResultScreen(true);
		}
		
		gameUIController.PlayerDamageHandler(currentLifes);
		gameUIController.PlayerEnemyKilledHandler((float)CurrentProgress / (float)MaxProgress);
	}
	
	private void PlayerEnemyKilledHandler(int coinsToAdd)
	{
		if (currentProgress + coinsToAdd >= maxProgress)
		{
			currentProgress = maxProgress;
			gamePreferences.IncreaseLevelSave();
			DisablePlayer();
			gameUIController.AppearGameResultScreen(false);
		}
		else
		{
			currentProgress += coinsToAdd;
		}
		
		gameUIController.PlayerEnemyKilledHandler((float)CurrentProgress / (float)MaxProgress);
	}
	
	private void DisablePlayer()
	{
		spawner.Disable();
		playerMovementController.DisablePlayerControls();
		playerMovementController.SetDefaultTimeScale();
		spawner.PopAllEnemies();
		UnsubscribePlayer();
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
