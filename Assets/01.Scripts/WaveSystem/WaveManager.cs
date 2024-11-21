using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoSingleton<WaveManager>
{
	[SerializeField] private WaveDataListSO _waveDataListSO;
	[SerializeField] private EnemyGenerator[] _enemyGenerators;

	private WaveDataSO _curWaveDataSO;

	//Events
	public event Action<int> OnGenerateStartEvent;
	public event Action OnGenerateEndEvent;

	//Values
	public int EnemyCount {get; private set;}
	public int WaveNumber {get; private set;}

	private void Start()
	{
		//���� ����
		OnGenerate();
	}

	public void OnGenerate()
	{
		StartCoroutine(CoroutineGenerateRoop(_waveDataListSO[WaveNumber]));
	}

	private IEnumerator CoroutineGenerateRoop(WaveDataSO waveDataSO)
	{
		if(WaveNumber == 0) 
			yield return new WaitForSeconds(4.0f);

		//EnemyCount ����ϱ�
		EnemyCount = 0;
		foreach (var waveData in waveDataSO.wave)
			EnemyCount += waveData.enemyCount;

		OnGenerateStartEvent?.Invoke(EnemyCount);

		//���̺� ����
		yield return StartCoroutine(CoroutineEnemyGenerate(waveDataSO));

		//���� �� óġ�� ������ ��ٸ���
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
