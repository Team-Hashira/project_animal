using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoSingleton<BuildingManager>
{
    [SerializeField] private BuildingDataSO _buildingDataSO;
    private EBuildingType _buildingPreviewType;
    private List<Building> _curBuildings = new List<Building>();

    public bool IsBuildMove { get; private set; }

    public void OnBuildMode(EBuildingType buildingType)
    {
        _buildingPreviewType = buildingType;
        IsBuildMove = true;
    }

    public void OffBuildMode()
    {
        IsBuildMove = false;
    }

    public Building CreateBuilding(EBuildingType buildingType, Vector2 position, Transform parent = null)
    {
		Building building = Instantiate(_buildingDataSO[buildingType].building, position, Quaternion.identity);
		_curBuildings.Add(building);

        building.transform.SetParent(parent);

        return building;
    }

    public bool RemoveBuilding(Building building)
    {
        bool isContain = _curBuildings.Contains(building);

        if (isContain)
            _curBuildings.Remove(building);

        Destroy(building);

        return isContain;
    }
}
