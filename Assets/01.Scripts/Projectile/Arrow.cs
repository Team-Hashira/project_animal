using Crogen.CrogenPooling;
using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour, IPoolingObject
{
    private ProjectileBoxCollider2D _collider;
    private Entity _owner;
    private LayerMask _whatIsTarget;
    private float _speed;
    private int _damage;
    private bool _isEnable;


    private float _lifetime = 3f;
    private float _popTime;

    public string OriginPoolType { get; set; }
    GameObject IPoolingObject.gameObject { get; set; }

    private void Awake()
    {
        _collider = GetComponent<ProjectileBoxCollider2D>();
    }

    public void Init(Entity owner, LayerMask whatIsTarget, float speed, int damage)
    {
        _owner = owner;
        _whatIsTarget = whatIsTarget;
        _speed = speed;
        _damage = damage;
    }

    private void FixedUpdate()
    {
        if (_lifetime + _popTime < Time.time)
        {
            this.Push();
            _isEnable = false;
        }

        if (_isEnable == false) return;

        Vector3 movement = transform.up * Time.fixedDeltaTime;
        if (_collider.CheckCollision(_whatIsTarget, movement))
        {
            foreach (var hit in _collider.GetHits())
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.GetCompo<HealthCompo>().ApplyDamage(_damage);
                    this.Push();
                }
                _isEnable = false;
                transform.position += transform.up * hit.distance;
                break;
            }
        }
        else
        {
            transform.position += movement;
        }
    }

    public void OnPop()
    {
        _isEnable = true;
        _popTime = Time.time;
    }

    public void OnPush()
    {

    }
}
