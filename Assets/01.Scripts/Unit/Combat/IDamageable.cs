using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(float attackCoefficient, EStatType baseStatType, Stat harmerStat);
    //IKnockback���� ���°� ���� ��
    //public void ApplyKnockback(Vector2 knockbackDir, float knockbackPower);
}
