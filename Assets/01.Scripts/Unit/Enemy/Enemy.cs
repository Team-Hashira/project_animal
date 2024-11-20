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

	//Compo
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
		
	}

	public void OnPush()
	{
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
