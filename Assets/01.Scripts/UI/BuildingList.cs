using UnityEngine;
using UnityEngine.UI;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingDataSO;
    [SerializeField] private Button _buildingBtnPrefab;
    [SerializeField] private BuildingPreview _buildingPreview;
    private UserToolArea _userToolArea;

    public void Init(UserToolArea userToolArea)
    {
        _userToolArea = userToolArea;


        foreach (EBuildingType buildingType in _buildingDataSO.buildingPrefabs.Keys)
        {
            Button buildingBtn = Instantiate(_buildingBtnPrefab, transform);
            buildingBtn.onClick.AddListener(() => 
            {
                BuildingManager.Instance.OnBuildMode(buildingType);
                _buildingPreview.SetBuilding(buildingType);
            });
        }
    }
}
