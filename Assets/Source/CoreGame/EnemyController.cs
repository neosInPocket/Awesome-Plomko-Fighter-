using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private EnemySpike spike;
	[SerializeField] private float speed;
	[SerializeField] private GameObject explosionEffect;
	
	private void Start()
	{
		
	}
	
	public void TakeDamage()
	{
		StartCoroutine("TakeDamageEffect");
	}
	
	private IEnumerator TakeDamageEffect()
	{
		var expEffect = Instantiate(explosionEffect, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
