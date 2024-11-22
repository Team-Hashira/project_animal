using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SelectRect : MonoBehaviour
{
    [SerializeField] private InputReaderSO _input;

    private Vector3 _startPos;
    private bool _isDrag;
    private SpriteRenderer _spriteRenderer;
    private Collider2D[] _colliders = new Collider2D[100];
    private Collider2D[] _selectedObj;

    [SerializeField] private ContactFilter2D _whatIsTarget;

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

            //드래그 끝났을 때 실행되는 코드
        }
    }

    public void Update()
    {
        if (_isDrag == false) return;
        SizeSetting();

        //드래그일 때 실행되는 코드
         Physics2D.OverlapBox(transform.position, transform.localScale, 0, _whatIsTarget, _colliders);

        if (_selectedObj == null) return;
        if (_colliders.Length > 0)
        {
			_colliders.Except(_selectedObj).ToList().ForEach(coll =>
            {
                if (coll == null) return;
                if (coll.TryGetComponent(out ISelectable selectable))
                {
                    selectable.Select(true);
                }
            });
        }
        if (_selectedObj.Length > 0)
        {
            _selectedObj.Except(_colliders).ToList().ForEach(coll =>
            {
				if (coll == null) return;
				if (coll.TryGetComponent(out ISelectable selectable))
                {
                    selectable.Select(false);
                }
            });
        }
        _selectedObj = _colliders;
    }

    private void SizeSetting()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(_input.MousePosition);
        mousePos.z = 0;
        Vector3 scale = mousePos - _startPos;
        scale.x = Mathf.Abs(scale.x);
        scale.y = Mathf.Abs(scale.y);
        scale.z = Mathf.Abs(scale.z);
        transform.localScale = scale;
        transform.position = (mousePos + _startPos) / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
