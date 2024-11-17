using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EStatType
{
    MaxHealth,
    Defense,
    ReceiveDamagePercent,
    Damage,
    Critical,
    CriticalPercent,
    InflictDamagePercent,
    Speed,
    Jump,
}

[System.Serializable]
public struct StatInfo
{
    public EStatType statType;
    public float defaultValue;
}

public class Stat : MonoBehaviour
{
    [SerializeField] private StatSO _stat;

    public StatElement GetElement(EStatType statType)
    {
        return _stat.GetStatElement(statType);
    }

    public float GetValue(EStatType statType)
    {
        StatElement stat = _stat.GetStatElement(statType);
        if (stat != null)
        {
            return stat.GetValue();
        }
        else
        {
            string statName = statType.ToString();
            if (statName.IndexOf("Percent") != -1)
                return 1;
            else
                return 0;
        }
    }
}
