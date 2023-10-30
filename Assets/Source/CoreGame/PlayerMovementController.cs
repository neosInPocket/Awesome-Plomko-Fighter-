using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private TrajectoryPredictor trajectoryPredictor;
	
	private void Start()
	{
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
		trajectoryPredictor.SetFingerStartPosition(finger.screenPosition);
	}
	
	private void OnFingerMoveHandler(Finger finger)
	{
		trajectoryPredictor.SetPredictionLength(finger.screenPosition);
	}
	
	private void OnFingerUpHandler(Finger finger)
	{
		trajectoryPredictor.HideTrajectory();
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
