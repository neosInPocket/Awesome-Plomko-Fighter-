using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private SpriteRenderer spriteRenderer; 
	[SerializeField] private TrailRenderer trailRenderer; 
	[SerializeField] private GameObject playerDeathEffect;
	private PlayerData player;
	public PlayerData Player => player;
	private bool isInvincible;
	public Rigidbody2D Rigid => rigid;
	public bool IsInvincible
	{
		get => isInvincible;
		set => isInvincible = value;
	}
	public TrailRenderer TrailRenderer => trailRenderer;
	
	public void Initialize()
	{
		var color = new Color(1, 1, 1, 1);
		spriteRenderer.color = color;
		trailRenderer.startColor = new Color(1, 0.5618984f, 0.4764151f, 1);
		player = new PlayerData();
		player.DamageTaken += PlayerDamageTakenHandler;
		spriteRenderer.enabled = true;
		trailRenderer.enabled = true;
	}
	
	public void SubscribeDamageEvent(Action<int> subscriber)
	{
		player.DamageTaken += subscriber;
	}
	
	public void SubscribeEnemyKillEvent(Action<int> subscriber)
	{
		player.EnemyKilled += subscriber;
	}
	
	private void PlayerDamageTakenHandler(int currentLifes)
	{
		if (currentLifes == 0)
		{
			StartCoroutine(PopDeathEffect());
		}
		else
		{
			StartCoroutine(PopDamageEffect());
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
		for (int i = 0; i < 5; i++)
		{
			float alpha = 1;
			Color color;
			
			while (alpha > 0)
			{
				alpha -= 0.05f;
				color = new Color(1, 1, 1, alpha);
				spriteRenderer.color = color;
				trailRenderer.startColor = new Color(1, 0.5618984f, 0.4764151f, alpha);
				yield return new WaitForFixedUpdate();
			}
			
			while (alpha < 1)
			{
				alpha += 0.05f;
				color = new Color(1, 1, 1, alpha);
				spriteRenderer.color = color;
				trailRenderer.startColor = new Color(1, 0.5618984f, 0.4764151f, alpha);
				yield return new WaitForFixedUpdate();
			}
		}
	}
	
	public void UnSubscribeDamageEvent(Action<int> subscriber)
	{
		player.DamageTaken -= subscriber;
	}
	
	public void UnSubscribeEnemyKillEvent(Action<int> subscriber)
	{
		player.EnemyKilled -= subscriber;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		var spike = collision.gameObject.GetComponent<EnemySpike>();
		
		if (!spike) return;
		if (IsInvincible) return;
		
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		if (spike.transform.parent.position.y < 0)
		{
			transform.position = new Vector2(0, screenSize.y * 3 / 4);
			trailRenderer.Clear();
			rigid.velocity = Vector2.zero;
			rigid.angularVelocity = 0;
		}
		player.TakeDamage();
	}
}
