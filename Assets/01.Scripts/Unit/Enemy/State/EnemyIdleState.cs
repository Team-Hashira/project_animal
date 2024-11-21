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
		_surfaceMovementCompo.ImmediatelyStop();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void Update()
	{
		base.Update();
		if (_surfaceMovementCompo.FindTarget() != null)
			_stateMachine.ChangeState(EEnemyState.Move);
	}
}
