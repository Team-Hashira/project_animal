using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoSingleton<BuildingManager>
{
    [field:SerializeField] public Building CoreBuilding {get; private set;}
    [SerializeField] private BuildingDataSO _buildingDataSO;
    private List<Building> _curBuildings = new List<Building>();

    public bool IsBuildMove { get; private set; }

	private void Awake()
	{
	}

	public void OnBuildMode()
    {
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
