using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusController : MonoBehaviour
{
	[SerializeField] private List<Image> heartImages;
	[SerializeField] private GameStatusBar gameStatusBar;
	[SerializeField] private TMP_Text levelText;
	
	private void Start()
	{
		GamePreferences preferences = new GamePreferences();
		RefreshLifes(preferences.PlayerMaximumLifesUpgrades);
		SetLevel(preferences.PlayerLevelSave);
	}
	
	public void RefreshStatus(float fullValue)
	{
		gameStatusBar.RefreshFill(fullValue);
	}
	
	public void SetLevel(int levelNumber)
	{
		levelText.text = $"LEVEL {levelNumber}";
	}
	
	public void RefreshLifes(int activeLifes)
	{
		foreach (var lifes in heartImages)
		{
			lifes.enabled = false;
		}
		
		for (int i = 0; i < activeLifes; i++)
		{
			heartImages[i].enabled = true;
		}
	}
}
