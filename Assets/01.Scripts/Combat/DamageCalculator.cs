using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalculator
{
    /// <summary>
    /// 최종 데미지를 계산합니다.
    /// </summary>
    /// <param name="attackCoefficient">공격 계수</param>
    /// <param name="baseStatType">기반 스탯 (공격력 비례 or 체력 비례 등등)</param>
    /// <param name="harmerStat">공격자 스탯</param>
    /// <param name="targetStat">피격자 스탯</param>
    /// <returns></returns>
    public static float CalculateDamage(float attackCoefficient,
                                        EStatType baseStatType,
                                        Stat harmerStat,
                                        Stat targetStat)
    {
        //내 데미지 적용
        float finalDamage = harmerStat.GetValue(baseStatType);

        //적 방어력 적용
        finalDamage = Mathf.Log10((float)finalDamage / targetStat.GetValue(EStatType.Defense) * 10);

        //크리티컬 적용
        bool isCritical = Random.Range(0f, 1f) < harmerStat.GetValue(EStatType.CriticalPercent);
        float criticalValue = isCritical ? harmerStat.GetValue(EStatType.Critical) : 1;
        finalDamage *= criticalValue;

        //공격 계수 적용
        finalDamage *= attackCoefficient;

        //주는 데미지 적용
        finalDamage *= harmerStat.GetValue(EStatType.InflictDamagePercent);

        //적 받는 데미지 적용
        finalDamage *= targetStat.GetValue(EStatType.ReceiveDamagePercent);

        return finalDamage;
    }
}
