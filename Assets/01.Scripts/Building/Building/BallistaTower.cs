using Crogen.CrogenPooling;
using System;
using UnityEngine;

public class BallistaTower : Building
{
    [Header("Target setting")]
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private float _radius;
    [SerializeField] private Transform _ballistaTrm;

    [Header("Shoot setting")]
    [SerializeField] private float _shootDelay;
    private float _currentCountUp;

    [Header("Arrow setting")]
    [SerializeField] private ProjectilePoolType _arrow;
    [SerializeField] private int _arrowdamage;
    [SerializeField] private float _arrowSpeed;

    private Unit _target;
    private WorkBarCompo _workBarCompo;

    private Collider2D[] hits;

    protected override void Awake()
    {
        base.Awake();

        _workBarCompo = GetCompo<WorkBarCompo>();
        _workBarCompo.ShowWorkBar(() => _currentCountUp / _shootDelay);
    }

    protected override void Update()
    {
        base.Update();

        _currentCountUp += Time.deltaTime;

        hits = Physics2D.OverlapCircleAll(_ballistaTrm.position, _radius, _whatIsEnemy);
        if (hits.Length != 0 && hits[0].TryGetComponent<Unit>(out _target))
        {
            _ballistaTrm.rotation = Quaternion.LookRotation(Vector3.back, _target.transform.position - _ballistaTrm.position);

            if (_currentCountUp > _shootDelay)
            {
                _currentCountUp = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Arrow arrow = gameObject.Pop(_arrow, _ballistaTrm.position, _ballistaTrm.rotation) as Arrow;
        arrow.Init(this, _whatIsEnemy, _arrowSpeed, _arrowdamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_ballistaTrm.position, _radius);
    }
}
