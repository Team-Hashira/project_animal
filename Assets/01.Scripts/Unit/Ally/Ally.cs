using System;
using UnityEngine;

public class Ally : Unit, ISelectable
{
	private SurfaceMovementCompo _surfaceMovementCompo;
    private bool _isSelected;
    [SerializeField] private InputReaderSO _inputReaderSO;

	public void Select(bool onSelect)
    {
        GetCompo<VisualCompo>().OutlineActive(onSelect);
        _isSelected = onSelect;
    }

	public void SelectComplete()
	{
	}

	protected override void Awake()
    {
        base.Awake();
		_inputReaderSO.OnLeftClickEvnet += HandleMoveToMousePos;
		_surfaceMovementCompo = GetCompo<SurfaceMovementCompo>();
		_stateMachine = new StateMachine(this);
    }

    private void HandleMoveToMousePos(bool value)
	{
        if (_isSelected == false) return;
        if(value == true ) return;

        var pos = Camera.main.ScreenToWorldPoint(_inputReaderSO.MousePosition);

        _surfaceMovementCompo.SetDestination(pos);
	}
}
