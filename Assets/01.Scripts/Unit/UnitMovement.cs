using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UnitMovement : MonoBehaviour, IUnitInitializeComponent
{
    private InputReaderSO _input;
    private Rigidbody2D _rigid;
    private Unit _owner;

    public Vector2 Movement { get; private set; }

    [SerializeField] private float _speed = 10;

    public void Initialize(Unit unit)
    {
        _owner = unit;
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = Movement;
    }

    public void Move(Vector2 vector)
    {
        Movement = vector.normalized * _speed;
    }

    public void StopImmediate()
    {
        Movement = Vector2.zero;
    }
}
