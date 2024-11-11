using UnityEngine;

public class PlayerMoveState : UnitState
{
    public PlayerMoveState(Unit owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
    {
    }
}
