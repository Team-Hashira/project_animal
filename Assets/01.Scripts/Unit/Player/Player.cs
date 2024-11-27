using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EPlayerState
{
    Idle, Move
}

public class Player : Unit
{
    [field:SerializeField] public InputReaderSO Input { get; private set; }

    [SerializeField] private Transform _targetArrow;
    [SerializeField] private DamageCaster2D _damageCaster;
    private Transform _visualTrm;
    private Vector3 _originScale;

    protected override void Awake()
    {
        base.Awake();
        _visualTrm = transform.Find("VisualPivot/Visual");
        _originScale = _visualTrm.localScale;
		Input.OnLeftClickEvnet += HandleLeftClickEvent;
		Input.OnMouseMoveEvent += HandleMouseMoveEvent;
        _damageCaster.OnDamageCastSuccessEvent += HandleAttackSuccessEvent;
    }

	private void Start()
	{
        GetCompo<HealthCompo>().OnDieEvent += GameManager.Instance.GameOver;
	}

	private void HandleMouseMoveEvent(Vector2 pos)
	{
        pos = Camera.main.ScreenToWorldPoint(pos);
        Vector2 dir = (pos - (Vector2)transform.position).normalized;
        _visualTrm.localScale = 
            new Vector3(-Mathf.Sign(dir.x) * 
            _originScale.x,
            _originScale.y,
			_originScale.z);
	}

	private void HandleAttackSuccessEvent()
    {
        CameraManager.Instance.ShakeCamera(3, 3, 0.1f);
    }

    private void HandleLeftClickEvent(bool isDown)
    {
        if (isDown == false | UIManager.Instance.OnUIMouse || UIManager.Instance.IsBuildingMode) return;

        _damageCaster.CastDamage(10, EStatType.Damage, GetCompo<StatCompo>());
    }

    protected override void Update()
    {
        base.Update();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.MousePosition);
        mousePos.z = 0;
        Vector3 dir = (mousePos - transform.position).normalized;

        _targetArrow.rotation = Quaternion.LookRotation(Vector3.back, dir);
    }
}
