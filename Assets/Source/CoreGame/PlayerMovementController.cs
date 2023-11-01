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
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		defaultDeltaTime = Time.fixedDeltaTime;
	}
	
	public void EnablePlayerControls()
	{
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
		Touch.onFingerDown -= OnFingerDownHandler;
		Touch.onFingerMove -= OnFingerMoveHandler;
		Touch.onFingerUp -= OnFingerUpHandler;
	}
	
	private void OnDestroy()
	{
		DisablePlayerControls();	
	}
	
	public void SetDefaultTimeScale()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = defaultDeltaTime;
	}
}
