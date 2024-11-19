using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UserToolArea : MonoBehaviour
{
    [SerializeField] private Button _buildingBtn;
    [SerializeField] private BuildingList _buildingList;

    private bool _isShowed;
    private Sequence _moveSeq;
    private RectTransform _rectTrm;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;

        _buildingBtn.onClick.AddListener(ToolArea);
    }

    private void ToolArea()
    {
        if (_isShowed)
            HideToolArea();
       else
            ShowToolArea();
    }

    private void ShowToolArea()
    {
        _isShowed = true;

        if (_moveSeq != null && _moveSeq.IsActive()) _moveSeq.Kill();
        _moveSeq = DOTween.Sequence();

        _moveSeq.Append(_rectTrm.DOAnchorPosY(200, 0.2f));
    }

    private void HideToolArea()
    {
        _isShowed = false;

        if (_moveSeq != null && _moveSeq.IsActive()) _moveSeq.Kill();
        _moveSeq = DOTween.Sequence();

        _moveSeq.Append(_rectTrm.DOAnchorPosY(0, 0.2f));
    }
}