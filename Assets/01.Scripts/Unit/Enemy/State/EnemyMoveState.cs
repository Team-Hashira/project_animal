using UnityEngine;

public class EnemyMoveState : UnitState<Enemy>
{
	SurfaceMovementCompo _surfaceMovementCompo;
	FindTargetCompo _findTargetCompo;

	public EnemyMoveState(Enemy owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
	{
		_surfaceMovementCompo = owner.GetCompo<SurfaceMovementCompo>();
		_findTargetCompo = owner.GetCompo<FindTargetCompo>();
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		Collider2D closestCollider = _findTargetCompo.FindClosestCollider();
		if (closestCollider != null)
		{
			_surfaceMovementCompo.SetDestination(closestCollider.transform.position);
		}
		else
		{
			_surfaceMovementCompo.SetDestination(BuildingManager.Instance.CoreBuilding.transform.position);
		}
	}
}
