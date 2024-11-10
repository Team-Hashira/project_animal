using UnityEngine;

public class ResourceGenerator : BuildingModifier
{
	public float delay = 1;
	public EResourceType resourceType;
	public int resourceAmount = 1;

	private ResourceManager _resourceManager;

	public override void Init(Building owner)
	{
		base.Init(owner);
		_resourceManager = ResourceManager.Instance;
	}

	private void Update()
	{
		_resourceManager.AddResource(resourceType, resourceAmount);
	}
}
