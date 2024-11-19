using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EControlsType
{
    Player
}

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, Controls.IPlayerActions
{
    private Controls _controls;

    #region Actions
    public Action<Vector2> OnMoveEvnet;
    public Action<bool> OnLeftClickEvnet;
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

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnLeftClickEvnet?.Invoke(true);
        else if (context.canceled)
            OnLeftClickEvnet?.Invoke(false);
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }
}
