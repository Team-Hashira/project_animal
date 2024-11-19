using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceListSO", menuName = "SO/Resource/List")]
public class ResourceListSO : ScriptableObject
{
    [SerializeField] private List<ResourceSO> _resourceList;
    private Dictionary<EResourceType, ResourceSO> _resourceDict;

    private void OnEnable()
    {
        if (_resourceDict == null)
        {
            _resourceDict = new Dictionary<EResourceType, ResourceSO>();
        }
        for (int i = 0; i < _resourceList.Count; i++)
        {
            if (_resourceDict.ContainsKey(_resourceList[i].resourceType)) continue;
            _resourceDict.Add(_resourceList[i].resourceType, _resourceList[i]);
        }
    }

    public ResourceSO GetResourceSO(EResourceType resourceType)
    {
        if (_resourceDict.ContainsKey(resourceType)) 
            return _resourceDict[resourceType];
        Debug.LogWarning($"Do not exist ResourceSO for {resourceType}");
        return null;
    }
}
