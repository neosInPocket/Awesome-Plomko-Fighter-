using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreScreenControlling : MonoBehaviour
{
	[SerializeField] private TMP_Text playerCoinstext;
	[SerializeField] private Button lifesButton;
	[SerializeField] private Button impulseButton;
	[SerializeField] private Image[] lifesPoints;
	[SerializeField] private Image[] impulsePoints;
	private GamePreferences gamePreferences;
	
	private void Start()
	{
		gamePreferences = new GamePreferences();
		RefreshShop();
	}
	
	public void RefreshShop()
	{
		playerCoinstext.text = gamePreferences.PlayerBank.ToString();
		
		RefreshImpulseButton();
		RefreshLifesButton();
		
		RefreshImpulseUpgrade();
		RefreshLifesUpgrade();
	}
	
	public void UpgradeLifes()
	{
		gamePreferences.IncreasePlayerBank(-100);
		gamePreferences.IncreaseLifesUpgrade();
		RefreshLifesUpgrade();
		RefreshLifesButton();
		RefreshImpulseButton();
		playerCoinstext.text = gamePreferences.PlayerBank.ToString();
	}
	
	public void UpgradeImpulse()
	{
		gamePreferences.IncreasePlayerBank(-50);
		gamePreferences.IncreaseImpulseUpgrade();
		RefreshImpulseUpgrade();
		RefreshImpulseButton();
		RefreshLifesButton();
		playerCoinstext.text = gamePreferences.PlayerBank.ToString();
	}
	
	private void RefreshImpulseUpgrade()
	{
		foreach (var point in impulsePoints)
		{
			point.enabled = false;
		}
		
		for (int i = 0; i < gamePreferences.PlayerImpulseUpgrade; i++)
		{
			impulsePoints[i].enabled = true;
		}
	}
	
	private void RefreshLifesUpgrade()
	{
		foreach (var life in lifesPoints)
		{
			life.enabled = false;
		}
		
		for (int i = 0; i < gamePreferences.PlayerMaximumLifesUpgrades; i++)
		{
			lifesPoints[i].enabled = true;
		}
	}
	
	private void RefreshLifesButton()
	{
		if (gamePreferences.PlayerBank >= 100)
		{
			lifesButton.interactable = true;
		}
		else
		{
			lifesButton.interactable = false;
		}
	}
	
	private void RefreshImpulseButton()
	{
		if (gamePreferences.PlayerBank >= 50)
		{
			impulseButton.interactable = true;
		}
		else
		{
			impulseButton.interactable = false;
		}
	}
}
