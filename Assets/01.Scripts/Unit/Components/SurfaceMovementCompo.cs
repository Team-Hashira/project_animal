using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SurfaceMovementCompo : MonoBehaviour, IAfterInitComponent
{
	private NavMeshAgent _agent;
	[SerializeField] private LayerMask _whatIsFindable;

	private void Awake()
	{
	}

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{

	}

	public void FindAndSetTarget()
	{
		SetTarget(FindTarget());
	}	

	public void SetTarget(Vector2 target)
	{
		_agent ??= GetComponent<NavMeshAgent>();
		_agent.SetDestination(target);
	}

	public Vector2 FindTarget()
	{
		Vector2 target = transform.position;
		for (int i = 0; i < 10; i++)
		{
			var trm = Physics2D.OverlapCircle(transform.position, i * 5, _whatIsFindable)?.transform;
			if(trm != null)
			target = trm.position;
		}
		return target;
	}

	void Update()
    {
        
    }
}
