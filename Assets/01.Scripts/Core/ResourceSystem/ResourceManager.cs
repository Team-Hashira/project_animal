using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    [SerializeField] private SerializedDictionary<EResourceType, int> _resourceCountDict;

    public event Action<EResourceType, int> OnResourceChangedEvent;

    public int GetResourceCount(EResourceType resourceType)
    {
        if(_resourceCountDict.TryGetValue(resourceType, out int count))
            return count;

        return 0;
    }

    public void AddResource(EResourceType resourceType, int count)
    {
        if (_resourceCountDict.TryAdd(resourceType, count) == false)
        {
            _resourceCountDict[resourceType] += count;
            OnResourceChangedEvent?.Invoke(resourceType, _resourceCountDict[resourceType]);
        }
	}

    public bool TryRemoveResource(EResourceType resourceType, int count)
    {
        if (_resourceCountDict.ContainsKey(resourceType) &&
            _resourceCountDict[resourceType] > count)
        {
            _resourceCountDict[resourceType] -= count;
            OnResourceChangedEvent?.Invoke(resourceType, _resourceCountDict[resourceType]);
            return true;
        }

        return false;
    }
}
