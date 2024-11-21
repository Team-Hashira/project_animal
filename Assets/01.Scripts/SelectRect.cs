using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SelectRect : MonoBehaviour
{
    [SerializeField] private InputReaderSO _input;

    private Vector3 _startPos;
    private bool _isDrag;
    private SpriteRenderer _spriteRenderer;
    private Collider2D[] _colliders;

    [SerializeField] private LayerMask _whatIsTarget;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

        _input.OnRightClickEvnet += HandleRightClickEvent;
    }

    private void HandleRightClickEvent(bool isDown)
    {
        if (isDown)
        {
            _startPos = Camera.main.ScreenToWorldPoint(_input.MousePosition);
            _startPos.z = 0;
            transform.localScale = Vector3.zero;
            _spriteRenderer.enabled = true;
            _isDrag = true;
        }
        else
        {
            transform.localScale = Vector3.zero;
            _spriteRenderer.enabled = false;
            _isDrag = false;
        }
    }

    public void Update()
    {
        if (_isDrag)
        {
            SizeSetting();

            _colliders = Physics2D.OverlapBoxAll(transform.position, transform.lossyScale, 0, _whatIsTarget);
        }
    }

    private void SizeSetting()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(_input.MousePosition);
        mousePos.z = 0;
        transform.localScale = mousePos - _startPos;
        transform.position = (mousePos + _startPos) / 2;
    }
}
