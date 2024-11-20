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
        }
        OnResourceChangedEvent?.Invoke(resourceType, _resourceCountDict[resourceType]);
    }

    public bool CanUseResource(EResourceType resourceType, int tryCount)
    {
        if (_resourceCountDict.TryGetValue(resourceType, out int count))
        {
            return count >= tryCount;
        }
        return false;
    }

    public int RemoveResource(EResourceType resourceType, int count)
    {
        int over = 0;
        if (_resourceCountDict.ContainsKey(resourceType))
        {
            if (_resourceCountDict[resourceType] < count)
            {
                over = count - _resourceCountDict[resourceType];
                _resourceCountDict[resourceType] = 0;
            }
            else
            {
                _resourceCountDict[resourceType] -= count;
                over = _resourceCountDict[resourceType];
            }
            OnResourceChangedEvent?.Invoke(resourceType, _resourceCountDict[resourceType]);
        }

        return over;
    }
}
