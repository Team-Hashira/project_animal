using UnityEngine;

public abstract class BuildingModifier : MonoBehaviour
{
	protected Building _owner;

	public void Init(Building owner)
	{
		_owner = owner;
	}
}
