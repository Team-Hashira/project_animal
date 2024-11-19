using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField] private InputReaderSO _input;
    [SerializeField] private EBuildingType _buildingPreviewType;

    private bool _isActived = false;
    private bool _isVisible = false;

    public void SetBuilding(EBuildingType buildingType)
    {
        _isVisible = true;
        _buildingPreviewType = buildingType;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        BuildingManager.Instance.CreateBuilding(_buildingPreviewType, transform.position, transform);
    }
    public void SetBuilding()
    {
        _isVisible = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void HandleLeftClickEvent(bool isDown)
    {
        if (isDown == false && _isVisible && !EventSystem.current.IsPointerOverGameObject())
        {
            BuildingManager.Instance.CreateBuilding(_buildingPreviewType, transform.position);
            _isActived = false;
            //_input.OnLeftClickEvnet -= HandleLeftClickEvent;
            //BuildingManager.Instance.OffBuildMode();
            //SetBuilding();
        }
    }

    private void Update()
    {
        if (BuildingManager.Instance.IsBuildMove == false) return;

        if (_isActived == false)
            _input.OnLeftClickEvnet += HandleLeftClickEvent;

        _isActived = true;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(_input.MousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
    }
}
