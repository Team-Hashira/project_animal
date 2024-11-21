using System;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
	[SerializeField] private EnemyGenerator[] _enemyGenerators;
	public event Action<int> OnGenerateEnemyEvent;

	private void Awake()
	{
		
	}
}
