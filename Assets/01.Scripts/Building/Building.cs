using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Building : Entity
{
	private BoxCollider2D _boxCollider;
	private SpriteRenderer[] _visualSpriteRenderers;
    [SerializeField] private LayerMask _whatIsNonOverlap;

	private bool _isPreview;
	public bool IsMakeablePosition { get; private set; }

	public void SetPreview()
	{
		_isPreview = true;
    }

    protected override void Awake()
	{
		base.Awake();
        _visualSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        if (_isPreview)
        {
            Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position, _boxCollider.size * 1.1f, 0, _whatIsNonOverlap);
            IsMakeablePosition = coll.Length <= 1;
            foreach (SpriteRenderer renderer in _visualSpriteRenderers)
            {
                renderer.color = IsMakeablePosition ? new Color(1, 1, 1, 0.5f) : new Color(1, 0.35f, 0.35f, 0.5f);
            }
        }
    }
}
