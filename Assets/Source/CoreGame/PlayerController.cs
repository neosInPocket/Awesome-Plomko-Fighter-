using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer; 
	[SerializeField] private TrailRenderer trailRenderer; 
	[SerializeField] private GameObject playerDeathEffect;
	[SerializeField] private GameObject playerDamageEffect;
	private PlayerData player;
	public PlayerData Player => player;
	
	public void Initialize()
	{
		player = new PlayerData();
		player.DamageTaken += PlayerDamageTakenHandler;
	}
	
	public void SubscribeDamageEvent(Action<bool> subscriber)
	{
		player.DamageTaken += subscriber;
	}
	
	public void SubscribeEnemyKillEvent(Action<int> subscriber)
	{
		player.EnemyKilled += subscriber;
	}
	
	private void PlayerDamageTakenHandler(bool isDead)
	{
		if (isDead)
		{
			StartCoroutine("PopDeathEffect");
		}
		else
		{
			StartCoroutine("PopDamageEffect");
		}
	}
	
	private IEnumerator PopDeathEffect()
	{
		spriteRenderer.enabled = false;
		trailRenderer.enabled = false;
		var death = Instantiate(playerDeathEffect, transform.position, Quaternion.identity, transform);
		
		yield return new WaitForSeconds(1f);
		Destroy(death.gameObject);
	}
	
	private IEnumerator PopDamageEffect()
	{
		for (int i = 0; i == 3; i++)
		{
			
		}
		
		yield return new WaitForSeconds(.01f);
	}
	
	public void UnSubscribeDamageEvent(Action<bool> subscriber)
	{
		player.DamageTaken -= subscriber;
	}
	
	public void UnSubscribeEnemyKillEvent(Action<int> subscriber)
	{
		player.EnemyKilled -= subscriber;
	}
}
