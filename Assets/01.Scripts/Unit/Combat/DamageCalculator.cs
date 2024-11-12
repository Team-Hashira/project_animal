using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    /// <summary>
    /// ���� �������� ����մϴ�.
    /// </summary>
    /// <param name="attackCoefficient">���� ���</param>
    /// <param name="baseStatType">��� ���� (���ݷ� ��� or ü�� ��� ���)</param>
    /// <param name="harmerStat">������ ����</param>
    /// <param name="targetStat">�ǰ��� ����</param>
    /// <returns></returns>
    public static float CalculateDamage(float attackCoefficient,
                                        EStatType baseStatType,
                                        Stat harmerStat,
                                        Stat targetStat)
    {
        //�� ������ ����
        float finalDamage = harmerStat.GetValue(baseStatType);

        //�� ���� ����
        finalDamage = Mathf.Log10((float)finalDamage / targetStat.GetValue(EStatType.Defense) * 10);

        //ũ��Ƽ�� ����
        bool isCritical = Random.Range(0f, 1f) < harmerStat.GetValue(EStatType.CriticalPercent);
        float criticalValue = isCritical ? harmerStat.GetValue(EStatType.Critical) : 1;
        finalDamage *= criticalValue;

        //���� ��� ����
        finalDamage *= attackCoefficient;

        //�ִ� ������ ����
        finalDamage *= harmerStat.GetValue(EStatType.InflictDamagePercent);

        //�� �޴� ������ ����
        finalDamage *= targetStat.GetValue(EStatType.ReceiveDamagePercent);

        return finalDamage;
    }
}
