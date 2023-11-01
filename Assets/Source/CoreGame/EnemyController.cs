using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] TrailRenderer trailRenderer;
	[SerializeField] private EnemySpike spike;
	[SerializeField] private CircleCollider2D circleCollider;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private GameObject spawnEffect;
	[SerializeField] EnemyMovementController enemyMovementController;
	public SpriteRenderer SP => spriteRenderer;
	public EnemyMovementController EnemyMovementController => enemyMovementController;
	public Action<EnemyController> Dead;
	private bool isDead;
	
	private void Start()
	{
		StartCoroutine(Effect(spawnEffect, false));
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		var player = collision.gameObject.GetComponent<PlayerController>();
		var spike = collision.otherCollider.gameObject.GetComponent<EnemySpike>();
		
		if (spike) return;
		if (player != null && !isDead)
		{
			enemyMovementController.Rigid.angularVelocity = 0;
			enemyMovementController.Rigid.velocity = Vector2.zero;
			Dead?.Invoke(this);
			isDead = true;
			player.Player.KillEnemy();
			TakeDamage();
		}
	}
	
	public void TakeDamage()
	{
		StartCoroutine(Effect(explosionEffect, true));
	}
	
	private IEnumerator Effect(GameObject effect, bool isDestroy)
	{
		var expEffect = Instantiate(effect, transform.position, Quaternion.identity, transform);
		
		if (isDestroy)
		{
			spriteRenderer.color = new Color(1, 1, 1, 0);
			circleCollider.enabled = false;
			trailRenderer.enabled = false;
			spike.gameObject.SetActive(false);
		}
		
		yield return new WaitForSeconds(1f);
		
		if (isDestroy)
		{
			Destroy(gameObject);
		}
		else
		{
			Destroy(expEffect);
		}
		
	}
}
