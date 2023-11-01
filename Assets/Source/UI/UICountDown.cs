using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICountDown : MonoBehaviour
{
	public Action CountDownEnded;
	
	public void RaiseCountDonEnd()
	{
		CountDownEnded?.Invoke();
		gameObject.SetActive(false);
	}
	
	public void Play()
	{
		gameObject.SetActive(true);
	}
}
