using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WaveSystem/WaveDataListSO")]
public class WaveDataListSO : ScriptableObject
{
	public List<WaveDataSO> waveDataSOList;
}
