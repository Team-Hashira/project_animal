using UnityEngine;

public class ResourceGeneratorBuildingCompo : MonoBehaviour, IInitComponent
{
	public float delay = 1;
	public EResourceType resourceType;
	public int resourceAmount = 1;

	private ResourceManager _resourceManager;

    public void Initialize(Entity entity)
    {
        _resourceManager = ResourceManager.Instance;
    }

    private void Update()
	{
		_resourceManager.AddResource(resourceType, resourceAmount);
	}
}
