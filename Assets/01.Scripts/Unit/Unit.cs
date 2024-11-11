using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected StateMachine _stateMachine;

    protected virtual void Awake()
    {
        _stateMachine = new StateMachine(this);

        GetComponentsInChildren<IUnitInitializeComponent>().ToList()
            .ForEach(component => component.Initialize(this));

        GetComponentsInChildren<IUnitAfterInitComponent>().ToList()
            .ForEach(component => component.AfterInit());
    }

    protected virtual void Update()
    {
        _stateMachine.MachineUpdate();
    }

    protected virtual void OnDestroy()
    {
        GetComponentsInChildren<IUnitDisposeComponent>().ToList()
            .ForEach(component => component.Dispose());
    }
}
