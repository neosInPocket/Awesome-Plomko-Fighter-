using UnityEngine;
using Cinemachine;

[SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
public class FreezeCameraPosition : CinemachineExtension
{
    public float XPos = 0;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.x = XPos;
            state.RawPosition = pos;
        }
    }
}