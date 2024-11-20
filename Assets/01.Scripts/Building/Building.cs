using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Building : Entity
{
	private IBuildingComponent[] _buildingModifiers;
	private BoxCollider2D _boxCollider;
	[SerializeField] private SpriteRenderer _visualSpriteRenderer;
    [SerializeField] private LayerMask _whatIsBuilding;

	private bool _isPreview;
	public bool IsMakeablePosition { get; private set; }

	public void SetPreview()
	{
		_isPreview = true;
    }

    protected override void Awake()
	{
		base.Awake();
        _boxCollider = GetComponent<BoxCollider2D>();
        _buildingModifiers = GetComponentsInChildren<IBuildingComponent>();

		foreach (IBuildingComponent modifier in _buildingModifiers)
		{
			modifier.Init(this);
        }
    }

    private void Update()
    {
        if (_isPreview)
        {
            Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position, _boxCollider.size * 1.1f, 0, _whatIsBuilding);
            IsMakeablePosition = coll.Length <= 1;
            _visualSpriteRenderer.color = IsMakeablePosition ? new Color(1, 1, 1, 0.5f) : new Color(1, 0.35f, 0.35f, 0.5f);
        }
    }
}
