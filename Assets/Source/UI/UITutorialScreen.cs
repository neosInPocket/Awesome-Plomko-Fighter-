using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class UITutorialScreen : MonoBehaviour
{ 
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private Animator animator;
	
	public Action OnTutorialEnded;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Part1;
	}
	
	public void Show()
	{
		gameObject.SetActive(true);
		characterText.text = "Welcome to Awesome Plomko Fighter!";
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	
	public void RaiseTutorialEnded()
	{
		OnTutorialEnded?.Invoke();
	}
	
	private void Part1(Finger finger)
	{
		Touch.onFingerDown -= Part1;
		Touch.onFingerDown += Part2;
		characterText.text = "Control time and you ball by holding your screen!";
	}
	
	private void Part2(Finger finger)
	{
		Touch.onFingerDown -= Part2;
		Touch.onFingerDown += Part3;
		characterText.text = "You can launch your ball with choosing its trajectory";
	}
	
	private void Part3(Finger finger)
	{
		Touch.onFingerDown -= Part3;
		Touch.onFingerDown += Part4;
		characterText.text = "Pop your enemies by hitting their back, but be aware of their spikes";
	}
	
	private void Part4(Finger finger)
	{
		Touch.onFingerDown -= Part4;
		Touch.onFingerDown += Part5;
		characterText.text = "Complete levels, recieve rewards and buy different upgrades in shop!";
	}
	
	private void Part5(Finger finger)
	{
		Touch.onFingerDown -= Part5;
		Touch.onFingerDown += Part6;
		characterText.text = "Good luck!";
	}
	
	private void Part6(Finger finger)
	{
		Touch.onFingerDown -= Part6;
		RaiseTutorialEnded();
		animator.SetTrigger("Hide");
	}
}
