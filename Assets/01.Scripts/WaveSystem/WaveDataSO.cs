using Crogen.CrogenPooling;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WaveSystem/WaveDataSO")]
public class WaveDataSO : ScriptableObject
{
    public List<WaveData> wave;

    public int Count { get => wave.Count; }
    public WaveData this[int index]
    {
        get => wave[index];
	}
}
