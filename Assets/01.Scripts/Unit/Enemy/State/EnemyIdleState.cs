using UnityEngine;

public class EnemyIdleState : UnitState<Enemy>
{
	SurfaceMovementCompo _surfaceMovementCompo;

	public EnemyIdleState(Enemy owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
	{
		_surfaceMovementCompo = owner.GetCompo<SurfaceMovementCompo>();
	}

	public override void Enter()
	{
		base.Enter();
		_surfaceMovementCompo.StopImmediately();
		_stateMachine.ChangeState(EEnemyState.Move);
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
