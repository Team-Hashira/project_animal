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
    [SerializeField] private ProjectilePoolType _arrow;
    [SerializeField] private float _shootDelay;
    private float _currentTime;

    [Header("Animation")]
    [SerializeField] private SpriteRenderer _bowRenderer;
    [SerializeField] private SpriteRenderer _arrowRenderer;
    [SerializeField] private Sprite[] _ballistaSprites;
    [SerializeField] private Sprite[] _arrowSprites;

    private Unit _target;
    private WorkBarCompo _workBarCompo;
    private StatCompo _statCompo;

    private Collider2D[] hits;

    protected override void Awake()
    {
        base.Awake();

        _workBarCompo = GetCompo<WorkBarCompo>();
        _statCompo = GetCompo<StatCompo>();
        _workBarCompo.ShowWorkBar(() => _currentTime / _shootDelay);
    }

    protected override void Update()
    {
        base.Update();

        _currentTime += Time.deltaTime;

        hits = Physics2D.OverlapCircleAll(_ballistaTrm.position, _radius, _whatIsEnemy);
        if (hits.Length != 0 && hits[0].TryGetComponent<Unit>(out _target))
        {
            _ballistaTrm.rotation = Quaternion.LookRotation(Vector3.back, _target.transform.position - _ballistaTrm.position);

            if (_currentTime > _shootDelay)
            {
                _currentTime = 0;
                Shoot();
            }
        }
        int spriteIndex = Mathf.Clamp(Mathf.RoundToInt((_currentTime * 3) / _shootDelay), 0, 2);
        _bowRenderer.sprite = _ballistaSprites[spriteIndex];
        _arrowRenderer.sprite = _arrowSprites[spriteIndex];
    }

    private void Shoot()
    {
        Arrow arrow = gameObject.Pop(_arrow, _ballistaTrm.position, _ballistaTrm.rotation) as Arrow;
        arrow.Init(this, _whatIsEnemy, _statCompo.GetValue(EStatType.Speed), Mathf.RoundToInt(_statCompo.GetValue(EStatType.Damage)));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_ballistaTrm.position, _radius);
    }
}
