using System.Collections;
using System.Collections.Generic;
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
	
	
	public GamePreferences()
	{
		LoadPreferences();
		playerLevelSave = 1;
		SavePreferences();
	}
	
	private void SavePreferences()
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
	
	public void SetLifesUpgrade(int value)
	{
		playerMaximumLifesUpgrades = value;
		SavePreferences();
	}
	
	public void SetImpulseUpgrade(int value)
	{
		playerImpulseUpgrade = value;
		SavePreferences();
	}
	
	public void SetTutorialRequired(bool value)
	{
		isTutorialRequired = value;
		SavePreferences();
	}
}
