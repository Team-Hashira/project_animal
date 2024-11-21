using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceDataArea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _woodText, _stoneText, _ironText;
    [SerializeField] private ResourceDataSO _resourceDataSO;

    private Dictionary<EResourceType, TextMeshProUGUI> _textDict;

    private void Awake()
    {
        ResourceManager.Instance.OnResourceChangedEvent += HandleResourceChangedEvent;

        _textDict = new Dictionary<EResourceType, TextMeshProUGUI>
        {
            { EResourceType.Stone, _stoneText },
            { EResourceType.Iron, _ironText },
            { EResourceType.Wood, _woodText },
        };

        SetUpResourceText();
    }

    private void SetUpResourceText()
    {
        foreach (EResourceType resourceType in Enum.GetValues(typeof(EResourceType)))
        {
            _textDict[resourceType].text =
                $"{_resourceDataSO[resourceType].resourceName} : {ResourceManager.Instance.GetResourceCount(resourceType)}";
        }
    }

    private void HandleResourceChangedEvent(EResourceType resourceType, int count)
    {
        _textDict[resourceType].text =
            $"{_resourceDataSO[resourceType].resourceName} : {count}";
    }
}
