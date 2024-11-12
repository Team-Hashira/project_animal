using System;
using UnityEngine;

public class PlayerIdleState : UnitState<Player>
{
    public PlayerIdleState(Player owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _owner.GetCompo<Movement>().StopImmediate();

        _owner.Input.OnMoveEvnet += HandleMoveEvent;
    }

    private void HandleMoveEvent(Vector2 movement)
    {
        if (movement != Vector2.zero)
            _stateMachine.ChangeState(EPlayerState.Move);
    }

    public override void Exit()
    {
        base.Exit();

        _owner.Input.OnMoveEvnet -= HandleMoveEvent;
    }

    public override void Update()
    {
        base.Update();
    }
}
 