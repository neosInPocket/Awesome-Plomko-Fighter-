using Cinemachine;
using UnityEngine;

public class UpwardsCamera : CinemachineExtension
{
	private float currentHeight;

	protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, 
													   CinemachineCore.Stage stage, 
													   ref CameraState state, 
													   float deltaTime)
	{
		if (stage == CinemachineCore.Stage.Body)
		{
			Vector3 pos = state.FinalPosition;
			if (pos.y < currentHeight)
			{
				pos.y = currentHeight;
			}
			
			state.RawPosition = pos;
		}
	}
	
	private void Update()
	{
		currentHeight = transform.position.y;
	}
	
}