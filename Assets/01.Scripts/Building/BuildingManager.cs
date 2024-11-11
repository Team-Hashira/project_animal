using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingDataSO;
    private List<Building> _curBuildings;

    public Building CreateBuilding(EBuildingType buildingType, Vector2 position)
    {
		Building building = Instantiate(_buildingDataSO[buildingType], position, Quaternion.identity);
		_curBuildings.Add(building);
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
