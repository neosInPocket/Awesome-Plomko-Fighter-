using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] float maximumSpeed;
	[SerializeField] float rotateSpeed;
	public Transform Target { get; set; }
	private float rotationMultiplier;
	
	private void Update()
	{
		if (Target == null)
		{
			return;
		}
		
		var direction = (Target.position - transform.position).normalized;
		var normalizedDirection = new Vector3(direction.x, direction.y, 0);
		var angle = Vector2.Angle(normalizedDirection, transform.up);
		var deltaPhi = rotateSpeed * angle * Time.deltaTime;
		
		var crossProduct = Vector3.Cross(transform.up, direction).z;
		rotationMultiplier = (int)(crossProduct / Mathf.Abs(crossProduct));
		deltaPhi *= rotationMultiplier;
		
		var a11 = Mathf.Cos(deltaPhi);
		var a12 = -Mathf.Sin(deltaPhi);
		var a21 = Mathf.Sin(deltaPhi);
		var a22 = Mathf.Cos(deltaPhi);
		
		transform.up = new Vector2(a11 * transform.up.x + a12 * transform.up.y, a21 * transform.up.x + a22 * transform.up.y);
		
		rigid.velocity = transform.up * maximumSpeed;
	}
}
