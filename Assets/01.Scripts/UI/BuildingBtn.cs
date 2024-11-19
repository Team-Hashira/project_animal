using UnityEngine;

public class BuildingBtn : MonoBehaviour
{
    private Building _createTargetPrefab;

    public void Init(Building building)
    {
        _createTargetPrefab = building;
    }
}
