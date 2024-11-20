using UnityEngine;

public class ResourceGeneratorBuildingCompo : MonoBehaviour, IBuildingComponent
{
	public float delay = 1;
	public EResourceType resourceType;
	public int resourceAmount = 1;

	private ResourceManager _resourceManager;

	public void Init(Building owner)
	{
		_resourceManager = ResourceManager.Instance;
	}

	private void Update()
	{
		_resourceManager.AddResource(resourceType, resourceAmount);
	}
}
