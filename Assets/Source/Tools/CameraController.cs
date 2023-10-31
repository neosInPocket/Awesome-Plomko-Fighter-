using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private FreezeAllCameraPosition freezeAllCameraPosition;
	
	public void EnableFreeze()
	{
		freezeAllCameraPosition.enabled = true;
	}
	
	public void DisableFreeze()
	{
		freezeAllCameraPosition.enabled = false;
	}
}
