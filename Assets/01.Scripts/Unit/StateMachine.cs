using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachine
{
    public Unit _owner;
    public UnitState CurrentState => _stateDictionary[_currentStateEnum];

    private Dictionary<Enum, UnitState> _stateDictionary;
    public Enum _currentStateEnum;

    public StateMachine(Unit owner)
    {
        _owner = owner;

        _stateDictionary = new Dictionary<Enum, UnitState>();

        string unitName = _owner.name;
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
                UnitState state = Activator.CreateInstance(unitState, _owner, this, enumName) as UnitState;
                _stateDictionary.Add(item, state);
                if (_currentStateEnum == null)
                    _currentStateEnum = item;
            }
            catch (Exception e)
            {
                Debug.LogError($"No class [ {unitName + item.ToString()}State ]");
            }
        }
    }

    public void MachineUpdate()
    {
        CurrentState.Update();
    }

    public void ChangeState(Enum stateEnum)
    {
        CurrentState.Exit();
        _currentStateEnum = stateEnum;
        CurrentState.Enter();
    }
}
