using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SurfaceMovementCompo : MonoBehaviour, IAfterInitComponent
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Transform _moveDirArrowTrm;
	private Transform _targetTrm;

	public Vector2 MoveDirection { get; private set; }

	private FindTargetCompo _findTargetCompo;

	private void Update()
	{
		MoveDirection = (_agent.destination - transform.position).normalized;
		if (_moveDirArrowTrm != null)
			_moveDirArrowTrm.up = MoveDirection;
	}

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{
		_findTargetCompo = entity.GetCompo<FindTargetCompo>();
	}

	public void StopImmediately()
	{
		_agent.Warp(transform.position);
		_agent.isStopped = false;
	}

	public void SetDestination(Vector2 destination)
	{
		_agent.SetDestination(destination);
	}
}
