using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : Entity
{
    protected StateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this);
    }

    protected virtual void Update()
    {
        _stateMachine.MachineUpdate();
    }
}
