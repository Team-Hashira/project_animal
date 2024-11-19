using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LevelManager : MonoSingleton<LevelManager>
{
	public event Action<int> OnLevelUpEvent;

	private float _xp;
	[SerializeField] private float _maxXP = 10;
	[SerializeField] private int _level = 1;

	public float XP
	{
		get => _xp;
		set
		{
			 _xp = value;
			if (_xp > _maxXP)
			{
				_xp = 0;
				++Level;
				OnLevelUpEvent?.Invoke(Level);
				MaxXPUp();
			}
		}
	}

	public int Level
	{
		get => _level;
		private set
		{
			_level = value;
		}
	}

	private void Update()
	{
		//디버그용
		if(Input.GetKey(KeyCode.Space))
		{
			XP += 1.5f;
		}
	}

	private void MaxXPUp()
	{
		_maxXP *= 1.3f;
	}
}
