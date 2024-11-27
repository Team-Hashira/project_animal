using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum EControlsType
{
    Player, UI
}

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
{
    private Controls _controls;

    #region Actions
    public event Action<Vector2> OnMoveEvnet;
    public event Action<bool> OnLeftClickEvnet;
	public event Action<bool> OnRightClickEvnet;
    public event Action<Vector2> OnMouseMoveEvent;

    public event Action OnMapEvnet;
    public event Action<float> OnMouseScrollEvnet;
    #endregion

    #region Values
    public Vector2 MousePosition { get; private set; }
    #endregion


    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.UI.SetCallbacks(this);
        }
        _controls.Player.Enable();
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
        _controls.UI.Disable();
    }

    public void SetEnableAction(EControlsType type, bool value)
    {
        switch (type)
        {
            case EControlsType.Player:
                if (value) _controls.Player.Enable();
                else _controls.Player.Disable();
                break;
            case EControlsType.UI:
                if (value) _controls.UI.Enable();
                else _controls.UI.Disable();
                break;
        } 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvnet?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
		OnMouseMoveEvent?.Invoke(context.ReadValue<Vector2>());
		MousePosition = context.ReadValue<Vector2>();
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnLeftClickEvnet?.Invoke(true);
        else if (context.canceled)
            OnLeftClickEvnet?.Invoke(false);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnRightClickEvnet?.Invoke(true);
        else if (context.canceled)
            OnRightClickEvnet?.Invoke(false);
    }

    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnMapEvnet?.Invoke();
    }

    public void OnMouseScroll(InputAction.CallbackContext context)
    {
        Debug.Log("Scroll");
        OnMouseScrollEvnet?.Invoke(context.ReadValue<float>());
    }
}