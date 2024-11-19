using UnityEngine;
using Crogen.CrogenPooling;

public enum EEnemyState
{
	Idle, Move
}

public class Enemy : Unit, IPoolingObject
{
	public string OriginPoolType { get; set; }
	GameObject IPoolingObject.gameObject { get; set; }
	[SerializeField] private LayerMask _whatIsFindable;
	private Entity _curTarget;
	private HealthCompo _healthCompo;
	protected override void Awake()
	{
		base.Awake();
		_healthCompo = GetCompo<HealthCompo>();
		_healthCompo.OnDieEvent += OnDie;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_healthCompo.OnDieEvent -= OnDie;
	}

	public void OnPop()
	{
		_healthCompo.AfterInit();
		for (int i = 0; i < 10; i++)
		{
			_curTarget = Physics2D.OverlapCircle(transform.position, i * 5, _whatIsFindable)?.GetComponent<Entity>();
			if (_curTarget != null)
			{
				Debug.Log("Ã£¾Ò´Ù!");
				break;
			}
		}
	}

	public void OnPush()
	{
		_curTarget = null;
	}

	protected override void Update()
	{
		base.Update();
	}

	private void OnDie()
	{
		this.Push();
		Debug.Log("Á×À½");
	}
}
