using UnityEngine;

public class UnitState
{
    protected Unit _owner;
    protected StateMachine _stateMachine;

    private readonly int _AnimationHash;

    public UnitState(Unit owner, StateMachine stateMachine, string animationName)
    {
        _owner = owner;
        _stateMachine = stateMachine;

        _AnimationHash = Animator.StringToHash(animationName);
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
