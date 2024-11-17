using UnityEngine;

public class Building : Entity
{
	private BuildingModifier[] _buildingModifiers;

    protected override void Awake()
	{
		base.Awake();
		_buildingModifiers = GetComponentsInChildren<BuildingModifier>();

		foreach (BuildingModifier modifier in _buildingModifiers)
		{
			modifier.Init(this);
		}
	}
}
