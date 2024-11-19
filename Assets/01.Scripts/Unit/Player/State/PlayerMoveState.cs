using System;
using UnityEngine;

public class PlayerMoveState : UnitState<Player>
{
    private MovementCompo _movement;

    public PlayerMoveState(Player owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _movement = _owner.GetCompo<MovementCompo>();

        _owner.Input.OnMoveEvnet += HandleMoveEvent;
    }

    private void HandleMoveEvent(Vector2 movement)
    {
        if (movement != Vector2.zero)
            _movement.Move(movement);
        else
            _stateMachine.ChangeState(EPlayerState.Idle);
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
