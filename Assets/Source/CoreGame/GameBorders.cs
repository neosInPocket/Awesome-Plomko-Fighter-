using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBorders : MonoBehaviour
{
	[SerializeField] private Border bottom;
	[SerializeField] private Border left;
	[SerializeField] private Border right;
	private void Start()
	{
		var screenBorders = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		bottom.transform.position = new Vector2(0, - screenBorders.y - bottom.SpriteRenderer.bounds.size.y / 2);
		bottom.SpriteRenderer.size = new Vector2(screenBorders.x * 2, bottom.SpriteRenderer.bounds.size.y);
		
		left.transform.position = new Vector2(-screenBorders.x - left.SpriteRenderer.bounds.size.x / 2, 0);
		left.SpriteRenderer.size = new Vector2(left.SpriteRenderer.bounds.size.x, screenBorders.y * 2);
		
		right.transform.position = new Vector2(screenBorders.x + right.SpriteRenderer.bounds.size.x / 2, 0);
		right.SpriteRenderer.size = new Vector2(right.SpriteRenderer.bounds.size.x, screenBorders.y * 2);
	}
}
