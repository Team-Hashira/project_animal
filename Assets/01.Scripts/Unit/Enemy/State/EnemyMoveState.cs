using UnityEngine;

public class EnemyMoveState : UnitState<Enemy>
{
	SurfaceMovementCompo surfaceMovementCompo;

	public EnemyMoveState(Enemy owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
	{
		surfaceMovementCompo = owner.GetCompo<SurfaceMovementCompo>();
	}

	public override void Enter()
	{
		base.Enter();
		surfaceMovementCompo.SetTarget(BuildingManager.Instance.CoreBuilding.transform.position);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		base.Update();
	}
}
