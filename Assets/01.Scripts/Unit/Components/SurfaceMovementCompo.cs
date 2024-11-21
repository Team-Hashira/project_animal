using NavMeshPlus.Components;
using System;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SurfaceMovementCompo : MonoBehaviour, IAfterInitComponent
{
	[SerializeField] private float detectionRadius = 10f; // 탐색 반경
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private LayerMask _whatIsFindable;
	[SerializeField] private int _maxFindCount = 10;
	private Transform _targetTrm;
	private Collider2D[] _colliders;

	private void Awake()
	{
		_colliders = new Collider2D[_maxFindCount];
	}

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{

	}

	public void StopImmediately()
	{
		_agent.Warp(transform.position);
		_agent.isStopped = false;
	}

	public Transform MoveToTarget()
	{
		Array.Clear(_colliders, 0, _maxFindCount);
		Physics2D.OverlapCircle(transform.position, detectionRadius, new ContactFilter2D() { layerMask = _whatIsFindable, useLayerMask = true, useTriggers = true }, _colliders);
		Collider2D closestCollider;
		if (_colliders.Length > 0)
		{
			closestCollider = FindClosestCollider(_colliders);

			if (closestCollider != null)
			{
				_agent.SetDestination(closestCollider.transform.position);
				return closestCollider.transform;
			}
		}
		return null;
	}

	public void MoveToCoreTower()
	{
		_agent.SetDestination(BuildingManager.Instance.CoreBuilding.transform.position);
	}

	private Collider2D FindClosestCollider(Collider2D[] colliders)
	{
		Collider2D closest = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;

		foreach (Collider2D collider in colliders)
		{
			if(collider == null) continue;
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
