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

	protected override void Awake()
	{
		base.Awake();
	}

	public void OnPop()
	{
		for (int i = 0; i < 10; i++)
		{
			_curTarget = Physics2D.OverlapCircle(transform.position, i * 5, 0, _whatIsFindable)?.GetComponent<Entity>();
			if (_curTarget != null) break;
		}

		if (_curTarget != null)
		{
			Debug.Log("Ã£¾Ò´Ù!");
		}
	}

	public void OnPush()
	{
	}

	protected override void Update()
	{
		base.Update();
	}
}
