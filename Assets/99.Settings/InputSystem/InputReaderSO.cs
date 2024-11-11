using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        _controls = new Controls();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvnet?.Invoke(context.ReadValue<Vector2>());
    }
}
