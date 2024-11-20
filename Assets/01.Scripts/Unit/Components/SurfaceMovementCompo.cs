using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class SurfaceMovementCompo : MonoBehaviour, IAfterInitComponent
{
	private NavMeshAgent _agent;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	public void AfterInit()
	{
	}

	public void Initialize(Entity entity)
	{

	}

	public void SetTarget(Vector2 target)
	{
		_agent.SetDestination(target);
	}

    void Update()
    {
        
    }
}
