using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SurfaceMovementCompo : MonoBehaviour, IAfterInitComponent
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private LayerMask _whatIsFindable;
	private Transform _targetTrm;

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{

	}

	public void ImmediatelyStop()
	{
		_agent.velocity = Vector3.zero;
		_agent.isStopped = false;
	}

	public Transform FindAndSetTarget()
	{
		if (FindTarget() != null)
		{
			SetTarget(_targetTrm.position);
			return _targetTrm;
		}
		return null;
	}	

	public void SetTarget(Vector2 target)
	{
		_agent.isStopped = true;
		_agent.SetDestination(target);
	}

	public Transform FindTarget()
	{
		if (_targetTrm != null)
		{
			if (_targetTrm.gameObject.activeSelf == false)
				_targetTrm = null;

			return _targetTrm;
		}

		for (int i = 0; i < 10; i++)
		{
			_targetTrm = Physics2D.OverlapCircle(transform.position, i * 20, _whatIsFindable)?.transform;
			if (_targetTrm != null) continue;
		}

		return _targetTrm;
	}
}
