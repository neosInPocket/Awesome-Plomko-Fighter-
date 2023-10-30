using Cinemachine.Utility;
using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
	[SerializeField] private SpriteMask mask;
	[SerializeField] private float trajectorySpeed;
	private Vector2 fingerStartPosition;
	private float maxDistance;
	private float trajectoryLength;
	private int currentDirectionMultiplier;
	
	private void Start()
	{
		trajectoryLength = mask.bounds.size.x / 2;
	}
	
	public void SetFingerStartPosition(Vector2 position)
	{
		mask.transform.localPosition = Vector2.zero;
		maxDistance = 0;
		currentDirectionMultiplier = 0;
		fingerStartPosition = Camera.main.ScreenToWorldPoint(position);
	}
	
	public void SetPredictionLength(Vector2 currentFingerPosition)
	{
		Vector2 worldPos = Camera.main.ScreenToWorldPoint(currentFingerPosition);
		var distance = Vector2.Distance(worldPos, fingerStartPosition);
		
		var direction = (worldPos - fingerStartPosition).normalized;
		SetRotation(direction);
		
		var maskDistance = Vector2.Distance(Vector2.zero, mask.transform.localPosition);
		
		if (distance < maxDistance)
		{
			if (currentDirectionMultiplier == -1 && maskDistance > trajectoryLength)
			{
				mask.transform.Translate(-transform.right * Mathf.Abs(maskDistance - trajectoryLength), Space.World);
				return;
			}
			
			currentDirectionMultiplier = -1;
			mask.transform.Translate(-transform.right * (trajectoryLength - distance) * trajectorySpeed, Space.World);
		}
		
		if (distance > maxDistance)
		{
			if (currentDirectionMultiplier == 1 && maskDistance > trajectoryLength)
			{
				mask.transform.Translate(transform.right * Mathf.Abs(maskDistance - trajectoryLength), Space.World);
				return;
			}
			
			currentDirectionMultiplier = 1;
			mask.transform.Translate(transform.right * distance * trajectorySpeed, Space.World);
		}
		maxDistance = distance;
	}
	
	public void SetRotation(Vector2 direction)
	{
		transform.right = direction;
	}
}