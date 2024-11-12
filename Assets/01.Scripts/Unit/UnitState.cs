using UnityEngine;

public class UnitState<T> : UnitStateBase where T : Unit
{
    protected T _owner;

    private readonly int _AnimationHash;

    public UnitState(T owner, StateMachine stateMachine, string animationName)
    {
        _owner = owner;
        _stateMachine = stateMachine;

        _AnimationHash = Animator.StringToHash(animationName);
    }
}

public class UnitStateBase
{
    protected StateMachine _stateMachine;

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
