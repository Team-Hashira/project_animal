using UnityEngine;

public class AttackCompo : MonoBehaviour, IAfterInitComponent
{
	[SerializeField] private DamageCaster2D _damageCaster;
	[SerializeField] private int _damage;
	private StatCompo _statCompo;

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{
		_statCompo = entity.GetCompo<StatCompo>();
	}

	public void OnAttack()
	{
		_damageCaster.CastDamage(_damage, EStatType.Damage, _statCompo);
	}
}
