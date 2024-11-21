using System;
using UnityEngine;

public class MainTower : Building
{
    protected override void Awake()
    {
        base.Awake();
        GetCompo<HealthCompo>().OnDieEvent += HandleDieEvent;
    }

    private void HandleDieEvent()
    {
        Debug.Log("GameOver");
    }
}
