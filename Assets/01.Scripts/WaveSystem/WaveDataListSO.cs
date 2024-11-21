using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WaveSystem/WaveDataListSO")]
public class WaveDataListSO : ScriptableObject
{
	public List<WaveDataSO> waveDataSOList;

	public int Count => waveDataSOList.Count;
	public WaveDataSO this[int index] => waveDataSOList[index];
}
