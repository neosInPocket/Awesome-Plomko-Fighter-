using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private AudioSource musicSource;
	private GamePreferences gamePreferences;
	
	private void Start()
	{
		var value = PlayerPrefs.GetFloat("MusicVolume", 1f);
		musicSource.volume = value;
		slider.value = musicSource.volume;
	}
	
	public void SetMusicVolume(float value)
	{
		musicSource.volume = value;
	}
	
	public void SaveVolume()
	{
		GamePreferences.MusicVolume = slider.value;
		PlayerPrefs.SetFloat("MusicVolume", GamePreferences.MusicVolume);
	}
}
