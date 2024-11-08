using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<EResourceType, int> _resourceCountDict;

    public int GetResourceCount(EResourceType resourceType)
    {
        if(_resourceCountDict.TryGetValue(resourceType, out int count))
            return count;

        return 0;
    }

    public void AddResource(EResourceType resourceType, int count)
    {
        if(_resourceCountDict.TryAdd(resourceType, count) == false)
            _resourceCountDict[resourceType] += count;
	}
}
