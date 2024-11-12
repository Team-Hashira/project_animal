using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour, IUnitComponent
{
    private InputReaderSO _input;
    private Rigidbody2D _rigid;
    private Unit _owner;

    private Vector2 _movement;

    [SerializeField] private float _speed = 10;

    public void Initialize(Unit unit)
    {
        _owner = unit;
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = _movement;
    }

    public void Move(Vector2 vector)
    {
        _movement = vector.normalized * _speed;
    }

    public void StopImmediate()
    {
        _movement = Vector2.zero;
    }
}
