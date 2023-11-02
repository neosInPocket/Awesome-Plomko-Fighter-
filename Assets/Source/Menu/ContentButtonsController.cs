using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentButtonsController : MonoBehaviour
{
	[SerializeField] private RectTransform rectTransform;
	[SerializeField] private Image leftButton;
	[SerializeField] private Image rightButton;
	private float width = -934.2419f;
	
	private void Update()
	{
		if (rectTransform.offsetMin.x > width / 2)
		{
			leftButton.gameObject.SetActive(false);
			rightButton.gameObject.SetActive(true);
		}
		else
		{
			leftButton.gameObject.SetActive(true);
			rightButton.gameObject.SetActive(false);
		}
	}
}
