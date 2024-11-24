using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectRect : MonoBehaviour
{
    [SerializeField] private InputReaderSO _input;

    private Vector3 _startPos;
    private bool _isDrag;
    private SpriteRenderer _spriteRenderer;
    private Collider2D[] _colliders = new Collider2D[50];
    private List<Collider2D> _selectedCollList = new List<Collider2D>();

    [SerializeField] private ContactFilter2D _whatIsTarget;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

        _input.OnRightClickEvnet += HandleRightClickEvent;
    }

	private void OnDestroy()
	{
		_input.OnRightClickEvnet -= HandleRightClickEvent;
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
            foreach (var collider in _colliders)
            {
                if(collider == null) continue;
				if (collider.TryGetComponent(out ISelectable selectable))
				{
					selectable.SelectComplete();
				}
			}
        }
    }

    public void Update()
    {
        if (_isDrag == false) return;
        SizeSetting();

        //드래그일 때 실행되는 코드
        if (Physics2D.OverlapBox(transform.position, transform.localScale, 0, _whatIsTarget, _colliders) <= 0)
        {
            _colliders.Except(_selectedCollList).ToList().ForEach(coll =>
            {
                if (coll == null) return;
                if (coll.TryGetComponent(out ISelectable selectable))
                {
                    Debug.Log("End");
                    selectable.Select(false);
                }
            });
            _selectedCollList.Clear();
            Array.Clear(_colliders, 0, _colliders.Length);
        }
        else
        {
            _colliders.Except(_selectedCollList).ToList().ForEach(coll =>
            {
                if (coll == null) return;
                if (coll.TryGetComponent(out ISelectable selectable))
                {
                    Debug.Log("Start");
                    selectable.Select(true);
                    _selectedCollList.Add(coll);
                }
            });
        }
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
