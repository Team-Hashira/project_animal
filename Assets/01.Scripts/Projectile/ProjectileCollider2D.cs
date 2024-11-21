using UnityEngine;

public abstract class ProjectileCollider2D : MonoBehaviour
{
    protected RaycastHit2D[] _hits;

    public abstract bool CollisionCheck(Vector2 moveTo);
}
