using UnityEngine;

public class AllyPigDeadState : UnitState<AllyPig>
{
    public AllyPigDeadState(AllyPig owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
    {
    }
}
