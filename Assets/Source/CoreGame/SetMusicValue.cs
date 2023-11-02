using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicValue : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	
	private void Start()
	{
		var value = PlayerPrefs.GetFloat("MusicVolume", 1f);
		audioSource.volume = value;
	}
}
