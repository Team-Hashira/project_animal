using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDataSO", menuName = "SO/Building/BuildingDataSO", order = 0)]
public class BuildingDataSO : ScriptableObject
{
    [SerializedDictionary("BuildingType", "BuildingSO")]
    public SerializedDictionary<EBuildingType, BuildingSO> buildingDict = new SerializedDictionary<EBuildingType, BuildingSO>();

	private void Reset()
	{
		EBuildingType[] buildingTypes = Enum.GetValues(typeof(EBuildingType)) as EBuildingType[];

		for (int i = 0; i < buildingTypes.Length; ++i)
		{
			buildingDict.Add(buildingTypes[i], null);
		}
	}

	public BuildingSO this[EBuildingType buildingType]
	{
		get => buildingDict[buildingType];
		set => buildingDict[buildingType] = value;
	}
}
