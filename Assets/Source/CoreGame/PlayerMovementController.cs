using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private TrajectoryPredictor trajectoryPredictor;
	[SerializeField] private float timeScale;
	[SerializeField] private float impulseMagnitude;
	private float defaultDeltaTime;
	
	private void Start()
	{
		defaultDeltaTime = Time.fixedDeltaTime;
		EnablePlayerControls();
	}
	
	public void EnablePlayerControls()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += OnFingerDownHandler;
		Touch.onFingerMove += OnFingerMoveHandler;
		Touch.onFingerUp += OnFingerUpHandler;
	}
	
	private void OnFingerDownHandler(Finger finger)
	{
		Time.timeScale = timeScale;
		Time.fixedDeltaTime = 0.001f;
		trajectoryPredictor.SetFingerStartPosition(finger.screenPosition);
	}
	
	private void OnFingerMoveHandler(Finger finger)
	{
		trajectoryPredictor.SetPredictionLength(finger.screenPosition);
	}
	
	private void OnFingerUpHandler(Finger finger)
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = defaultDeltaTime;
		trajectoryPredictor.HideTrajectory();
		SetPlayerImpulse(trajectoryPredictor.CurrentDirection * impulseMagnitude);
	}
	
	private void SetPlayerImpulse(Vector2 force)
	{
		rigid.velocity = force * impulseMagnitude;
	}
	
	public void DisablePlayerControls()
	{
		EnhancedTouchSupport.Disable();
		TouchSimulation.Disable();
		Touch.onFingerDown -= OnFingerDownHandler;
		Touch.onFingerMove -= OnFingerMoveHandler;
		Touch.onFingerUp -= OnFingerUpHandler;
	}
	
	private void OnDestroy()
	{
		DisablePlayerControls();	
	}
}
