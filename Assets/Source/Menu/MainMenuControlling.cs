using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControlling : MonoBehaviour
{
	[SerializeField] private GameObject shopScreen;
	[SerializeField] private GameObject menuScreen;
	[SerializeField] private GameObject settingsScreen;
	
	private void Start()
	{
		//ClearSettings();
	}
	
	private void ClearSettings()
	{
		var gamePreferences = new GamePreferences();
		gamePreferences.ResetPreferences();
	}
	
	public void ShopScreenActive()
	{
		shopScreen.SetActive(true);
	}
	
	public void ShopScreenInActive()
	{
		shopScreen.SetActive(false);
	}
	
	public void MenuScreenActive()
	{
		menuScreen.SetActive(true);
	}
	
	public void MenuScreenInActive()
	{
		menuScreen.SetActive(false);
	}
	
	public void SettingsScreenActive()
	{
		settingsScreen.SetActive(true);
	}
	
	public void SettingsScreenInActive()
	{
		settingsScreen.SetActive(false);
	}
	
	public void LoadGameScene()
	{
		SceneManager.LoadScene("StartGameScene");
	}
}
