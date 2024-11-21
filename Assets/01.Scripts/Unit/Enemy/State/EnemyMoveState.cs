using UnityEngine;

public class EnemyMoveState : UnitState<Enemy>
{
	SurfaceMovementCompo _surfaceMovementCompo;

	public EnemyMoveState(Enemy owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
	{
		_surfaceMovementCompo = owner.GetCompo<SurfaceMovementCompo>();
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
		if(_surfaceMovementCompo.MoveToTarget() == null)
		{
			_surfaceMovementCompo.MoveToCoreTower();
		}
	}
}
