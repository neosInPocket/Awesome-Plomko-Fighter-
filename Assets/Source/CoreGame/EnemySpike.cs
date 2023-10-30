using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		var player = collision.gameObject.GetComponent<PlayerController>();
		
		if (!player) return;
		
		player.Player.TakeDamage();
	}
}
