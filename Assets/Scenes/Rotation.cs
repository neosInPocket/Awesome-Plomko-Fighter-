using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Rotation : MonoBehaviour
{
	[SerializeField] private float deltaAngle;
	[SerializeField] private float rotateSpeed;
	private Vector3 lastPoint;
	private int rotationMultiplier;
	
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Spawn;
	}
	
	private void Update()
	{
		var direction = (lastPoint - transform.position).normalized;
		var normalizedDirection = new Vector3(direction.x, direction.y, 0);
		var angle = Vector2.Angle(normalizedDirection, -transform.up);
		var deltaPhi = deltaAngle * angle * Time.deltaTime;
		
		var crossProduct = Vector3.Cross(-transform.up, direction).z;
		rotationMultiplier = (int)(crossProduct / Mathf.Abs(crossProduct)); 
		Debug.Log(rotationMultiplier);
		deltaPhi *= rotationMultiplier;
		
		var a11 = Mathf.Cos(deltaPhi);
		var a12 = -Mathf.Sin(deltaPhi);
		var a21 = Mathf.Sin(deltaPhi);
		var a22 = Mathf.Cos(deltaPhi);
		
		transform.up = new Vector2(a11 * transform.up.x + a12 * transform.up.y, a21 * transform.up.x + a22 * transform.up.y);
	}
	
	private void Spawn(Finger finger)
	{
		lastPoint = Camera.main.ScreenToWorldPoint(finger.screenPosition);
	}
	
	private void OnDestroy()
	{
		EnhancedTouchSupport.Disable();
		TouchSimulation.Disable();
		Touch.onFingerDown -= Spawn;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, lastPoint);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, -transform.up);
	}
}
