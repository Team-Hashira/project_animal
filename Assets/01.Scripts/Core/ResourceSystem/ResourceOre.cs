using System;
using UnityEngine;
using Crogen.CrogenPooling;

public class ResourceOre : Entity
{
    [SerializeField] private ResourceSO _resourceSO;
    [SerializeField] private int _resourceGetCount = 2;
    [SerializeField] private EffectPoolType _hitEffect, _getResourceEffect;

    private HealthCompo _healthCompo;

    protected override void Awake()
    {
        base.Awake();
        _healthCompo = GetCompo<HealthCompo>();
        _healthCompo.OnHealthChangedEvent += HandleHealthChangedEvent;
        _healthCompo.OnDieEvent += HandleDieEvent;
    }

    private void HandleHealthChangedEvent(int prevHealth, int newHealth, bool isVisible)
    {
        if (prevHealth > newHealth)
        {
            gameObject.Pop(_hitEffect, transform.position, Quaternion.identity);
        }
    }

    private void HandleDieEvent()
    {
        _healthCompo.Resurrection();
		gameObject.Pop(_getResourceEffect, transform.position, Quaternion.identity);
        ResourceManager.Instance.AddResource(_resourceSO.resourceType, _resourceGetCount);
    }
}
