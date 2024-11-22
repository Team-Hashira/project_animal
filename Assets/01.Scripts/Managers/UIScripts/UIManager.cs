using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager> 
{
    public bool OnUIMouse { get; private set; }
    public bool IsBuildingMode { get; private set; }
    public bool IsBuildingSettingMode { get; private set; }

    public void OnBuildingMode(bool isOn)
    {
        IsBuildingMode = isOn;
    }

    public void Update()
    {
        OnUIMouse = EventSystem.current.IsPointerOverGameObject();
    }
}
