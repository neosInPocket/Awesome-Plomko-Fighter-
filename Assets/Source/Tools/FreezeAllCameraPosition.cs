using UnityEngine;
using Cinemachine;

[SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
public class FreezeAllCameraPosition : CinemachineExtension
{
    public float XPos = 0;
    public float YPos = 0;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            var pos = state.RawPosition;
            pos.x = XPos;
            pos.y = YPos;
            state.RawPosition = pos;
        }
    }
}