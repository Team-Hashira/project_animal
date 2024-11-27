using System;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO _input;
    [SerializeField] private GameObject _fullMapObj, _miniMapObj;
    //[SerializeField] private RectTransform _fogMask;
    [SerializeField] private Camera _minimapCamera;
    private RectTransform _mapRectTrm;

    public bool IsOnFullMap { get; private set; } = false;

    [Header("Minimap setting")]
    [SerializeField] private Vector2 _minimapSizeClamp;

    private float _targetMinimapCameraSize;

    private Vector2 _mouseStartPos;
    private Vector2 _mapStartPos;

    private bool _isDrag;

    private void Start()
    {
        FullMapActive(false);

        _mapRectTrm = _fullMapObj.transform as RectTransform;
        _targetMinimapCameraSize = (_minimapSizeClamp.x + _minimapSizeClamp.y) / 2;

        _input.OnMapEvnet += HandleMapEvent;
        _input.OnMouseScrollEvnet += HandleMouseScrollEvent;
        _input.OnLeftClickEvnet += HandleLeftClickEvent;
    }

    private void OnDestroy()
    {
        _input.OnMapEvnet -= HandleMapEvent;
        _input.OnMouseScrollEvnet -= HandleMouseScrollEvent;
        _input.OnLeftClickEvnet -= HandleLeftClickEvent;
    }

    private void Update()
    {
        _minimapCamera.orthographicSize = Mathf.Lerp(_minimapCamera.orthographicSize,
            _targetMinimapCameraSize, Time.deltaTime * 8);
        //_fogMask.sizeDelta = Vector2.Lerp(_minimapCamera.orthographicSize, _targetMinimapCameraSize, Time.deltaTime * 8);

        if (_isDrag)
        {
            Vector2 mouseMoveValue = _input.MousePosition - _mouseStartPos;

            _mapRectTrm.anchoredPosition = _mapStartPos + mouseMoveValue;
        }
    }
    public void FullMapActive(bool isOnFullMap)
    {
        _fullMapObj.SetActive(isOnFullMap);
        _miniMapObj.SetActive(!isOnFullMap);
    }

    private void HandleMouseScrollEvent(float value)
    {
        if (IsOnFullMap == false) return;

        _targetMinimapCameraSize -= value * 2;
        _targetMinimapCameraSize = Mathf.Clamp(_targetMinimapCameraSize,
            _minimapSizeClamp.x, _minimapSizeClamp.y);
    }

    private void HandleLeftClickEvent(bool isDown)
    {
        if (IsOnFullMap == false) return;

        _isDrag = isDown;
        if (isDown)
        {
            _mouseStartPos = _input.MousePosition;
            _mapStartPos = _mapRectTrm.anchoredPosition;
        }
    }

    private void HandleMapEvent()
    {
        IsOnFullMap = !IsOnFullMap;
        FullMapActive(IsOnFullMap);

        if (IsOnFullMap == false)
            _isDrag = false;
    }
}
