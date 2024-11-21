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
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		Debug.Log("Idle");
		base.Update();
		if (_surfaceMovementCompo.MoveToTarget() != null)
			_stateMachine.ChangeState(EEnemyState.Move);
	}
}
