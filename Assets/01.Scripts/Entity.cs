using Crogen.CrogenPooling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StatCompo), typeof(HealthCompo))]
public class Entity : MonoBehaviour, IDamageable
{
    private Dictionary<Type, IInitComponent> _compoDict;
    private List<IInitComponent> _initCompo;
    private List<IAfterInitComponent> _afterInitCompo;
    private List<IDisposeComponent> _disposeCompo;

    public event Action OnHitEvent;

    public void ApplyDamage(int attackCoefficient, EStatType baseStatType, StatCompo harmerStat)
    {
        //int damage = DamageCalculator.CalculateDamage(attackCoefficient, baseStatType, harmerStat, GetCompo<StatCompo>());
        GetCompo<HealthCompo>().ApplyDamage(attackCoefficient);
    }

    protected virtual void Awake()
    {
        _compoDict = new Dictionary<Type, IInitComponent>();

        _initCompo = GetComponentsInChildren<IInitComponent>().ToList();
        _afterInitCompo = GetComponentsInChildren<IAfterInitComponent>().ToList();
        _disposeCompo = GetComponentsInChildren<IDisposeComponent>().ToList();

        _initCompo.ForEach(component => _compoDict.Add(component.GetType(), component));

        Debug.Log(this is not IPoolingObject);
        if (this is not IPoolingObject pooling)
            InitComponent();
    }

    protected void InitComponent()
    {
        _initCompo.ForEach(component => component.Initialize(this));
        _afterInitCompo.ForEach(component => component.AfterInit());
    }
    protected void DisposeComponent()
    {
        _disposeCompo.ForEach(component => component.Dispose());
    }

    protected virtual void OnDestroy()
    {
        if (this is not IPoolingObject)
            DisposeComponent();
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
