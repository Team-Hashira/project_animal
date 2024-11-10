using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDataSO", menuName = "SO/Building/BuildingDataSO", order = 0)]
public class BuildingDataSO : ScriptableObject
{
	public SerializedDictionary<EBuildingType, Building> buildingPrefabs;

	private void Reset()
	{
		EBuildingType[] buildingTypes = Enum.GetValues(typeof(EBuildingType)) as EBuildingType[];

		for (int i = 0; i < buildingTypes.Length; ++i)
		{
			buildingPrefabs.Add(buildingTypes[i], null);
		}
	}


	public Building this[EBuildingType buildingType]
	{
		get => buildingPrefabs [buildingType];
		set => buildingPrefabs[buildingType] = value;
	}
}
