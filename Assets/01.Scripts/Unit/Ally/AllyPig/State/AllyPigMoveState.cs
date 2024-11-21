using UnityEngine;

public class AllyPigMoveState : UnitState<AllyPig>
{
    public AllyPigMoveState(AllyPig owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
    {
    }
}
