using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour, IHasStat
{
    [field:SerializeField] public Stat Stat { get; private set; }

    private Dictionary<Type, IInitComponent> _compoDict;

    protected virtual void Awake()
    {
        _compoDict = new Dictionary<Type, IInitComponent>();

        GetComponentsInChildren<IInitComponent>().ToList()
            .ForEach(component =>
            {
                _compoDict.Add(component.GetType(), component);
                component.Initialize(this);
            });

        GetComponentsInChildren<IAfterInitComponent>().ToList()
            .ForEach(component => component.AfterInit());
    }

    protected virtual void OnDestroy()
    {
        GetComponentsInChildren<IDisposeComponent>().ToList()
            .ForEach(component => component.Dispose());
    }

    public T GetCompo<T>(bool isDerived = false) where T : class, IInitComponent
    {
        if (_compoDict.TryGetValue(typeof(T), out IInitComponent compo))
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
