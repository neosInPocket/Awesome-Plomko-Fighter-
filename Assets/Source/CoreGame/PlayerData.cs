using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData
{
	private int lifes;
	private int impulseUpgrade;
	public int Lifes 
	{
		get => lifes;
		set => lifes = value;
	}
	public int ImpulseUpgrade => impulseUpgrade;
	
	public Action<bool> DamageTaken;
	public Action<int> EnemyKilled;
	
	public PlayerData()
	{
		var save = new GamePreferences();
		
		lifes = save.PlayerMaximumLifesUpgrades;
		impulseUpgrade = save.PlayerImpulseUpgrade;
	}
	
	public void TakeDamage()
	{
		if (lifes - 1 == 0)
		{
			DamageTaken?.Invoke(true);
			return;
		}
		else
		{
			DamageTaken?.Invoke(false);
		}
		
		lifes--;
	}
	
	public void KillEnemy()
	{
		EnemyKilled?.Invoke(2);
	}
}
