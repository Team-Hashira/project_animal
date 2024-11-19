using UnityEngine;
using UnityEngine.UI;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingDataSO;
    [SerializeField] private BuildingBtn _buildingBtnPrefab;
    [SerializeField] private BuildingBuilder _buildingPreview;
    private UserToolArea _userToolArea;

    public void Init(UserToolArea userToolArea)
    {
        _userToolArea = userToolArea;

        foreach (EBuildingType buildingType in _buildingDataSO.buildingDict.Keys)
        {
            BuildingBtn buildingBtn = Instantiate(_buildingBtnPrefab, transform);
            buildingBtn.Init(_buildingDataSO.buildingDict[buildingType], () => 
            {
                BuildingManager.Instance.OnBuildMode();
                _buildingPreview.SetBuilding(buildingType);
            });
        }
    }
}
