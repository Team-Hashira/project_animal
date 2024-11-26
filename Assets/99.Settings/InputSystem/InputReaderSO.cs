using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum EControlsType
{
    Player
}

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
{
    private Controls _controls;

    #region Actions
    public event Action<Vector2> OnMoveEvnet;
    public event Action<bool> OnLeftClickEvnet;
	public event Action<bool> OnRightClickEvnet;
    public event Action<Vector2> OnMouseMoveEvent;
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
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
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
}
