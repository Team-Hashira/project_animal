using DG.Tweening;
using UnityEngine;

public class EnemyMoveState : UnitState<Enemy>
{
	SurfaceMovementCompo _surfaceMovementCompo;
	FindTargetCompo _findTargetCompo;
	private Sequence _seq;

	public EnemyMoveState(Enemy owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
	{
		_surfaceMovementCompo = owner.GetCompo<SurfaceMovementCompo>();
		_findTargetCompo = owner.GetCompo<FindTargetCompo>();
	}

	public override void Enter()
	{
		base.Enter();
		base.Enter();
		_owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
		_seq = DOTween.Sequence();
		_seq.Append(_owner.VisualPivotTrm.DOLocalRotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
		_seq.Join(_owner.VisualPivotTrm.DOLocalMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));
		_surfaceMovementCompo.OnMoveEndEvent += HandleOnMoveEndEvent;
	}

	public override void Exit()
	{
		base.Exit();
		if (_seq != null && _seq.IsActive()) _seq.Kill();

		_owner.VisualPivotTrm.localEulerAngles = Vector3.one;
		_owner.VisualPivotTrm.localPosition = new Vector3(0, -0.25f, 0);
		_surfaceMovementCompo.OnMoveEndEvent -= HandleOnMoveEndEvent;
	}

	private void HandleOnMoveEndEvent()
	{
		_stateMachine.ChangeState(EAllyPigState.Idle);
	}

	public override void Update()
	{
		Collider2D closestCollider = _findTargetCompo.FindClosestCollider();
		Transform target;
		
		if (closestCollider != null)
		{
			target = closestCollider.transform;
		}
		else
		{
			target = BuildingManager.Instance.CoreBuilding.transform;
		}
		_surfaceMovementCompo.SetDestination(target.position);

		float dis = Vector2.Distance(target.position, _owner.transform.position);
		if (dis < 0.1f)
		{
			_surfaceMovementCompo.StopImmediately();
		}
	}
}
