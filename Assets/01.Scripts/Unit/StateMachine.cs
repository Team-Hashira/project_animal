using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachine
{
    public Unit _owner;

    private Dictionary<Enum, UnitStateBase> _stateDictionary;
    public Enum CurrentStateEnum { get; private set; }

    public StateMachine(Unit owner)
    {
        _owner = owner;

        _stateDictionary = new Dictionary<Enum, UnitStateBase>();

        string unitName = _owner.GetType().ToString();
        Type unitStateEnumType = Type.GetType("E" + unitName + "State");

        if (unitStateEnumType == null)
        {
            Debug.LogError($"No enum [ E{unitName}State ]");
            return;
        }

        foreach (Enum item in Enum.GetValues(unitStateEnumType))
        {
            string enumName = item.ToString();
            Type unitState = Type.GetType(unitName + enumName + "State");
            try
            {
                UnitStateBase state = Activator.CreateInstance(unitState, _owner, this, enumName) as UnitStateBase;
                _stateDictionary.Add(item, state);
                if (CurrentStateEnum == null)
                    CurrentStateEnum = item;
            }
            catch
            {
                Debug.LogError($"No class [ {unitName + item.ToString()}State ]");
            }
        }

        _stateDictionary[CurrentStateEnum].Enter();
    }

    public void MachineUpdate()
    {
        _stateDictionary[CurrentStateEnum].Update();
    }

    public void ChangeState(Enum stateEnum)
    {
        _stateDictionary[CurrentStateEnum].Exit();
        CurrentStateEnum = stateEnum;
        _stateDictionary[CurrentStateEnum].Enter();
    }
}
