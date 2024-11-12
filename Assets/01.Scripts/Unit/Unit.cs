using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected StateMachine _stateMachine;
    private Dictionary<Type, IUnitComponent> _compoDict;

    protected virtual void Awake()
    {
        _compoDict = new Dictionary<Type, IUnitComponent>();

        GetComponentsInChildren<IUnitComponent>().ToList()
            .ForEach(component =>
            {
                _compoDict.Add(component.GetType(), component);
                component.Initialize(this);
            });

        GetComponentsInChildren<IUnitAfterInitComponent>().ToList()
            .ForEach(component => component.AfterInit());

        _stateMachine = new StateMachine(this);
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

    public T GetCompo<T>(bool isDerived = false) where T : class, IUnitComponent
    {
        if (_compoDict.TryGetValue(typeof(T), out IUnitComponent compo))
        {
            return compo as T;
        }

        if (!isDerived) return default;

        Type findType = _compoDict.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
        if (findType != null)
            return _compoDict[findType] as T;

        return default;
    }
}
