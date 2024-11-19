using UnityEngine;

public class EnemyIdleState : UnitState<Enemy>
{
	public EnemyIdleState(Enemy owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
	{
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
		base.Update();
	}
}
