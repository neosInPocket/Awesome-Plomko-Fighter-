using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private CameraController cameraController;
	[SerializeField] private GameBorders borders;
	[SerializeField] EnemyController enemyPrefab;
	[SerializeField] Transform player;
	[SerializeField] float spawnDelta;
	[SerializeField] float spawnBoundsDelta;
	
	private float PlayerPosition => player.position.y;
	private float lastSpawnedPosition;
	private bool isFight;
	private List<EnemyController> currentEnemies = new List<EnemyController>();
	
	private void Start()
	{
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		lastSpawnedPosition = 3 * screenSize.y / 4;
	}
	
	private void Update()
	{
		if (isFight) return;
		
		Spawn();
	}
	
	private void Spawn()
	{
		currentEnemies.Clear();
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		var randomNumber = Random.Range(0, 2);
		if (randomNumber < 1)
		{
			var leftSpawnPosition = new Vector2(-screenSize.x + enemyPrefab.SP.bounds.size.x / 2 + spawnBoundsDelta, lastSpawnedPosition);
			var rightSpawnPosition = new Vector2(screenSize.x - enemyPrefab.SP.bounds.size.x / 2 - spawnBoundsDelta, lastSpawnedPosition);
			var left = Instantiate(enemyPrefab, leftSpawnPosition, enemyPrefab.transform.rotation, transform);
			left.EnemyMovementController.Target = player;
			var right = Instantiate(enemyPrefab, rightSpawnPosition, enemyPrefab.transform.rotation, transform);
			right.EnemyMovementController.Target = player;
			
			currentEnemies.Add(left);
			currentEnemies.Add(right);
			
			left.Dead += OnEnemyDeadHandler;
			right.Dead += OnEnemyDeadHandler;
		}	
		else
		{
			var spawnPosition = new Vector2(0, lastSpawnedPosition);
			var instance = Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation, transform);
			instance.EnemyMovementController.Target = player;
			currentEnemies.Add(instance);
			
			instance.Dead += OnEnemyDeadHandler;
		}
		
		isFight = true;
		cameraController.EnableFreeze();
		borders.TopBorder.gameObject.SetActive(true);
		lastSpawnedPosition = 3 * screenSize.y / 4;
	}
	
	private void OnEnemyDeadHandler(EnemyController enemyController)
	{
		enemyController.Dead -= OnEnemyDeadHandler;
		currentEnemies.Remove(enemyController);
		
		if (currentEnemies.Count == 0)
		{
			isFight = false;
			cameraController.DisableFreeze();
		}
	}
}
