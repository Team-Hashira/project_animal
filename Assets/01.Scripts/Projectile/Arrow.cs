using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Entity _owner;
    private LayerMask _whatIsTarget;
    private float _speed;

    public void Init(Entity owner, LayerMask whatIsTarget, float speed)
    {
        _speed= speed;
    }

    private void FixedUpdate()
    {
        
    }
}
