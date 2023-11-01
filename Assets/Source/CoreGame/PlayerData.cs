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
	
	public Action<int> DamageTaken;
	public Action<int> EnemyKilled;
	
	public PlayerData()
	{
		var save = new GamePreferences();
		
		//lifes = save.PlayerMaximumLifesUpgrades;
		lifes = 2;
		impulseUpgrade = save.PlayerImpulseUpgrade;
	}
	
	public void TakeDamage()
	{
		lifes--;
		DamageTaken?.Invoke(lifes);
	}
	
	public void KillEnemy()
	{
		EnemyKilled?.Invoke(2);
	}
}
