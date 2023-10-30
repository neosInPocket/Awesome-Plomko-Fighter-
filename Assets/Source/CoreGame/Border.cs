using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Border : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;   
	[SerializeField] private BoxCollider2D boxCollider;   
	
	public SpriteRenderer SpriteRenderer => spriteRenderer;  
	public BoxCollider2D BoxCollider => boxCollider;  
}
