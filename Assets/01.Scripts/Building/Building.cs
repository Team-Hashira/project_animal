using UnityEngine;

public class Building : MonoBehaviour
{
	private BuildingModifier[] _buildingModifiers;

	private void Awake()
	{
		_buildingModifiers = GetComponentsInChildren<BuildingModifier>();

		foreach (BuildingModifier modifier in _buildingModifiers)
		{
			modifier.Init(this);
		}
	}
}
