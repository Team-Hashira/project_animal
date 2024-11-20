using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementCompo : MonoBehaviour, IAfterInitComponent
{
    private Rigidbody2D _rigid;
    private Unit _owner;

    private Vector2 _movement;

    private StatElement _speedStat;

    public void Initialize(Entity entity)
    {
        _owner = entity as Unit;
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void AfterInit()
    {
        _speedStat = _owner.GetCompo<StatCompo>().GetElement(EStatType.Speed);
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = _movement;
    }

    public void Move(Vector2 vector)
    {
        if (_speedStat == null) return;

        _movement = vector.normalized * _speedStat.Value;
    }

    public void StopImmediate()
    {
        _movement = Vector2.zero;
    }
}
