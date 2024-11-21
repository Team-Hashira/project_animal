using UnityEngine;

public class ProjectileBoxCollider2D : ProjectileCollider2D
{
    public Vector2 size;
    public Vector2 offset;

    public override bool CheckCollision(LayerMask whatIsTarget, Vector2 moveTo = default)
    {
        _hits = Physics2D.BoxCastAll(transform.position + transform.rotation * offset,
                            size, transform.rotation.z, moveTo.normalized, moveTo.magnitude, whatIsTarget);

        return _hits.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix; 
        Gizmos.DrawWireCube(offset, size);
    }
}
