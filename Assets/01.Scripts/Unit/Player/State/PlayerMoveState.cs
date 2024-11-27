using DG.Tweening;
using System;
using UnityEngine;

public class PlayerMoveState : UnitState<Player>
{
    private MovementCompo _movement;
	private Sequence _seq;

	public PlayerMoveState(Player owner, StateMachine stateMachine, string animationName) : base(owner, stateMachine, animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _movement = _owner.GetCompo<MovementCompo>();
		_owner.VisualPivotTrm.localEulerAngles = new Vector3(0, 0, -7f);
		_seq = DOTween.Sequence();
		_seq.Append(_owner.VisualPivotTrm.DOLocalRotate(new Vector3(0, 0, 7f), 0.3f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo));
		_seq.Join(_owner.VisualPivotTrm.DOLocalMoveY(0.001f, 0.15f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo));
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
        _seq.Kill();
		_owner.VisualPivotTrm.localEulerAngles = Vector3.one;
		_owner.VisualPivotTrm.localPosition = new Vector3(0, -0.25f, 0);
		_owner.Input.OnMoveEvnet -= HandleMoveEvent;
    }

    public override void Update()
    {
        base.Update();
    }
}
