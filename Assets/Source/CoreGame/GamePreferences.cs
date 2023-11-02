using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class GamePreferences
{
	private int playerLevelSave;
	private int playerImpulseUpgrade;
	private int playerMaximumLifesUpgrades;
	private int playerBank;
	private bool isTutorialRequired;
	
	public int PlayerLevelSave => playerLevelSave;
	public int PlayerImpulseUpgrade => playerImpulseUpgrade;
	public int PlayerMaximumLifesUpgrades => playerMaximumLifesUpgrades;
	public int PlayerBank => playerBank;
	public bool IsTutorialRequired => isTutorialRequired;
	public static float MusicVolume;
	
	
	public GamePreferences()
	{
		LoadPreferences();
	}
	
	public void SavePreferences()
	{
		PlayerPrefs.SetInt("PlayerLevelSave", playerLevelSave);
		PlayerPrefs.SetInt("PlayerImpulseUpgrade", playerImpulseUpgrade);
		PlayerPrefs.SetInt("PlayerMaximumLifesUpgrades", playerMaximumLifesUpgrades);
		PlayerPrefs.SetInt("PlayerBank", playerBank);
		
		if (!IsTutorialRequired)
		{
			PlayerPrefs.SetInt("IsTutorialRequired", 0);
		}
		else
		{
			PlayerPrefs.SetInt("IsTutorialRequired", 1);
		}
		
	}
	
	private void LoadPreferences()
	{
		playerLevelSave = PlayerPrefs.GetInt("PlayerLevelSave", 1);
		playerImpulseUpgrade = PlayerPrefs.GetInt("PlayerImpulseUpgrade", 0);
		playerMaximumLifesUpgrades = PlayerPrefs.GetInt("PlayerMaximumLifesUpgrades", 1);
		playerBank = PlayerPrefs.GetInt("PlayerBank", 100);
		MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
		
		var isTutor = PlayerPrefs.GetInt("IsTutorialRequired", 1);
		
		if (isTutor == 0)
		{
			isTutorialRequired = false;
		}
		else
		{
			isTutorialRequired = true;
		}
	}
	
	public void IncreasePlayerBank(int bankToAdd)
	{
		playerBank += bankToAdd;
		SavePreferences();
	}
	
	public void IncreaseLevelSave()
	{
		playerLevelSave++;
		SavePreferences();
	}
	
	public void IncreaseLifesUpgrade()
	{
		playerMaximumLifesUpgrades++;
		SavePreferences();
	}
	
	public void IncreaseImpulseUpgrade()
	{
		playerImpulseUpgrade++;
		SavePreferences();
	}
	
	public void SetTutorialRequired(bool value)
	{
		isTutorialRequired = value;
		SavePreferences();
	}
	
	public void ResetPreferences()
	{
		PlayerPrefs.SetInt("PlayerLevelSave", 1);
		PlayerPrefs.SetInt("PlayerImpulseUpgrade", 0);
		PlayerPrefs.SetInt("PlayerMaximumLifesUpgrades", 1);
		PlayerPrefs.SetInt("PlayerBank", 100);
		PlayerPrefs.SetInt("IsTutorialRequired", 1);
		PlayerPrefs.SetFloat("MusicVolume", 1f);
		LoadPreferences();
	}
}
