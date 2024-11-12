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
    #endregion

    #region Values
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
}
