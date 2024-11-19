using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceListSO", menuName = "SO/Resource/List")]
public class ResourceDataSO : ScriptableObject
{
    [SerializedDictionary("ResourceType", "ResourceSO")]
    public SerializedDictionary<EResourceType, ResourceSO> resourceDict = new SerializedDictionary<EResourceType, ResourceSO>();

    public ResourceSO this[EResourceType resourceType]
    {
        get
        {
            if (resourceDict.ContainsKey(resourceType))
                return resourceDict[resourceType];
            Debug.LogWarning($"Do not exist ResourceSO for {resourceType}");
            return null;
        }
    }
}
