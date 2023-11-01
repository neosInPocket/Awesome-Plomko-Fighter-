using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] EnemyController enemyPrefab;
	[SerializeField] Transform player;
	[SerializeField] float spawnDelta;
	[SerializeField] float spawnBoundsDelta;
	private List<EnemyController> currentEnemies = new List<EnemyController>();
	private bool isEnabled;
	
	public void Spawn()
	{
		currentEnemies.Clear();
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		var randomNumber = Random.Range(0, 2);
		
		float ySpawnPos = 0;
		Quaternion rotation = Quaternion.identity;
			
		if (player.transform.position.y > 0)
		{
			ySpawnPos = -screenSize.y + enemyPrefab.SP.bounds.size.y / 2;
			rotation = Quaternion.Euler(0, 0, 0);
		}
		else
		{
			ySpawnPos = screenSize.y - enemyPrefab.SP.bounds.size.y / 2 - 0.15f * screenSize.y;
			rotation = Quaternion.Euler(0, 0, 180);
		}
		
		if (randomNumber < 1)
		{
			var leftSpawnPosition = new Vector2(-screenSize.x + enemyPrefab.SP.bounds.size.x / 2 + spawnBoundsDelta, ySpawnPos);
			var rightSpawnPosition = new Vector2(screenSize.x - enemyPrefab.SP.bounds.size.x / 2 - spawnBoundsDelta, ySpawnPos);
			var left = Instantiate(enemyPrefab, leftSpawnPosition, enemyPrefab.transform.rotation, transform);
			left.transform.rotation = rotation;
			left.EnemyMovementController.Target = player;
			var right = Instantiate(enemyPrefab, rightSpawnPosition, enemyPrefab.transform.rotation, transform);
			right.transform.rotation = rotation;
			right.EnemyMovementController.Target = player;
			
			currentEnemies.Add(left);
			currentEnemies.Add(right);
			
			left.Dead += OnEnemyDeadHandler;
			right.Dead += OnEnemyDeadHandler;
		}	
		else
		{
			var spawnPosition = new Vector2(-player.position.x, ySpawnPos);
			var instance = Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation, transform);
			instance.transform.rotation = rotation;
			instance.EnemyMovementController.Target = player;
			currentEnemies.Add(instance);
			
			instance.Dead += OnEnemyDeadHandler;
		}
	}
	
	private void OnEnemyDeadHandler(EnemyController enemyController)
	{
		enemyController.Dead -= OnEnemyDeadHandler;
		currentEnemies.Remove(enemyController);
		
		if (currentEnemies.Count == 0 && enabled)
		{
			Spawn();
		}
	}
	
	public void PopAllEnemies()
	{
		foreach (var enemy in currentEnemies)
		{
			enemy.TakeDamage();
		}
		
		currentEnemies.Clear();
	}
	
	public void Enable()
	{
		isEnabled = true;
	}
	
	public void Disable()
	{
		isEnabled = false;
	}
}
