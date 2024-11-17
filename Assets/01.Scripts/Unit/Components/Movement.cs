using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour, IInitComponent
{
    private InputReaderSO _input;
    private Rigidbody2D _rigid;
    private Unit _owner;

    private Vector2 _movement;

    [SerializeField] private float _speed = 10;

    public void Initialize(Entity entity)
    {
        _owner = entity as Unit;
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
