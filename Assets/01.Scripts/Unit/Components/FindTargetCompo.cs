using System;
using UnityEngine;
using UnityEngine.AI;

public class FindTargetCompo : MonoBehaviour, IAfterInitComponent
{
	[SerializeField] private float detectionRadius = 10f; // 탐색 반경
	[SerializeField] private LayerMask _whatIsFindable;
	[SerializeField] private int _maxFindCount = 10;
	private Transform _targetTrm;
	private Collider2D[] _colliders; 

	private ContactFilter2D _targetFilter;

	private void Awake()
	{
		_targetFilter = new ContactFilter2D() { layerMask = _whatIsFindable, useLayerMask = true, useTriggers = true };
		_colliders = new Collider2D[_maxFindCount];
	}


	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{

	}

	public Collider2D FindClosestCollider()
	{
		Array.Clear(_colliders, 0, _maxFindCount);

		Physics2D.OverlapCircle(transform.position, detectionRadius, _targetFilter, _colliders);

		Collider2D closest = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;

		foreach (Collider2D collider in _colliders)
		{
			if (collider == null) continue;
			float distanceSqr = (collider.transform.position - currentPosition).sqrMagnitude;
			if (distanceSqr < closestDistanceSqr)
			{
				closest = collider;
				closestDistanceSqr = distanceSqr;
			}
		}

		return closest;
	}

	void OnDrawGizmosSelected()
	{
		// 탐지 범위 시각화
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
