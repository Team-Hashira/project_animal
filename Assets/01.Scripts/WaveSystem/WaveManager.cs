using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
	[SerializeField] private WaveDataListSO _waveDataListSO;
	[SerializeField] private EnemyGenerator[] _enemyGenerators;

	private WaveDataSO _curWaveDataSO;

	//Events
	public event Action OnGenerateStartEvent;
	public event Action OnGenerateEndEvent;

	//Values
	public int EnemyCount {get; set;}
	public int WaveNumber {get; private set;}

	private void Start()
	{
		//생성 시작
		OnGenerate();
	}

	private void OnGenerate()
	{
		StartCoroutine(CoroutineGenerateRoop(_waveDataListSO[WaveNumber]));
	}

	private IEnumerator CoroutineGenerateRoop(WaveDataSO waveDataSO)
	{
		if(WaveNumber == 0) 
			yield return new WaitForSeconds(4.0f);

		//EnemyCount 계산하기
		EnemyCount = 0;
		foreach (var waveData in waveDataSO.wave)
			EnemyCount += waveData.enemyCount;

		OnGenerateStartEvent?.Invoke();

		//웨이브 실행
		yield return StartCoroutine(CoroutineEnemyGenerate(waveDataSO));

		//적들 다 처치할 때까지 기다리기
		yield return new WaitUntil(()=>EnemyCount <= 0);

		if(WaveNumber < _waveDataListSO.Count)
			OnGenerate();
	}

	private IEnumerator CoroutineEnemyGenerate(WaveDataSO waveDataSO)
	{
		for (int i = 0; i < waveDataSO.Count; i++)
		{
			WaveData curWaveData = waveDataSO[i];
			EnemyGenerator enemyGenerator = _enemyGenerators[curWaveData.generatePosIdx];

			for (int j = 0; j < curWaveData.enemyCount; j++)
			{
				enemyGenerator.OnGenerate(curWaveData.enemyType);
				yield return new WaitForSeconds(curWaveData.delayTime);
			}

			yield return new WaitForSeconds(curWaveData.nextTime);
		}
		OnGenerateEndEvent?.Invoke();
		++WaveNumber;
	}
}
