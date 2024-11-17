using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(float attackCoefficient, EStatType baseStatType, Stat harmerStat);
    //IKnockback으로 빼는게 좋을 듯
    //public void ApplyKnockback(Vector2 knockbackDir, float knockbackPower);
}
