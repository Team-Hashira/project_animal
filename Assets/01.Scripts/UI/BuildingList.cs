using UnityEngine;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private Building[] _buildings;
    [SerializeField] private BuildingBtn _buildingBtnPrefab;

    public void Init()
    {

    }

    private void Awake()
    {
        foreach (var building in _buildings)
        {
            BuildingBtn buildingBtn = Instantiate(_buildingBtnPrefab, transform);
            buildingBtn.Init(building);
        }
    }
}
