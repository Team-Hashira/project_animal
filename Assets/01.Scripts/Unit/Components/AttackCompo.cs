using UnityEngine;

public class AttackCompo : MonoBehaviour, IAfterInitComponent
{
	[SerializeField] private DamageCaster2D _damageCaster;
	[SerializeField] private int _damage;
	[SerializeField] private float _attackDelay = 0.4f;
	[SerializeField] private Transform _attackChargeVisualTrm;
	private float _curAttackTime = 0f;

	private StatCompo _statCompo;
	private SurfaceMovementCompo _surfaceMovementCompo;

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{
		_statCompo = entity.GetCompo<StatCompo>();
	}

	private void Update()
	{
		_attackChargeVisualTrm.localScale = Vector3.one * (_curAttackTime / _attackDelay);

		if (_attackDelay < _curAttackTime)
		{
			OnAttack();
			_curAttackTime = 0f;
		}
		else
		{
			_curAttackTime += Time.deltaTime;
		}
	}

	public void OnAttack()
	{
		_damageCaster.CastDamage(_damage, EStatType.Damage, _statCompo);
	}
}
