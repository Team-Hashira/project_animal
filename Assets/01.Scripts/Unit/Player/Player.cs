using System;
using UnityEngine;

public enum EPlayerState
{
    Idle, Move
}

public class Player : Unit
{
    [field:SerializeField] public InputReaderSO Input { get; private set; }

    [SerializeField] private Transform _targetArrow;
    [SerializeField] private DamageCaster2D _damageCaster;

    protected override void Awake()
    {
        base.Awake();
        Input.OnLeftClickEvnet += HandleLeftClickEvent;
        _damageCaster.OnDamageCastSuccessEvent += HandleAttackSuccessEvent;
    }

    private void HandleAttackSuccessEvent()
    {
        CameraManager.Instance.ShakeCamera(3, 3, 0.1f);
    }

    private void HandleLeftClickEvent(bool isDown)
    {
        if (isDown)
        {
            _damageCaster.CastDamage(10, EStatType.Damage, GetCompo<StatCompo>());
        }
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
