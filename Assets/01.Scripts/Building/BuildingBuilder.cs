using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBuilder : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingDataSO;
    [SerializeField] private InputReaderSO _input;
    [SerializeField] private EBuildingType _buildingPreviewType;

    private bool _isVisible = false;
    private bool _onUIMouse;
    private Building _buildingPreview;

    public void SetBuilding(EBuildingType buildingType)
    {
        if (_isVisible == false)
        {
            _isVisible = true;

            _input.OnLeftClickEvnet += HandleLeftClickEvent;
            _input.OnRightClickEvnet += HandleRightClickEvent;
        }
        _buildingPreviewType = buildingType;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        _buildingPreview = BuildingManager.Instance.CreateBuilding(_buildingPreviewType, transform.position, transform);
        _buildingPreview.SetPreview();
    }
    public void SetBuilding()
    {
        if (_isVisible)
        {
            _isVisible = false;

            _input.OnLeftClickEvnet -= HandleLeftClickEvent;
            _input.OnRightClickEvnet -= HandleRightClickEvent;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        _buildingPreview = null;
    }

    private void HandleLeftClickEvent(bool isDown)
    {
        if (isDown == false) return;
        if (_buildingPreview == null) return;
        if (_buildingPreview.IsMakeablePosition == false)
        {
            Debug.Log("°ø°£ÀÌ ¾øÀÝ¾Æ ºýÅë¾Æ");
            return;
        }

        BuildingSO buildingSO = _buildingDataSO[_buildingPreviewType];
        if (_isVisible && !_onUIMouse)
        {
            bool canCreate = buildingSO.recipe.Keys.All(resourceType => 
                ResourceManager.Instance.CanUseResource(resourceType, buildingSO.recipe[resourceType]));
            if (canCreate)
            {
                buildingSO.recipe.Keys.ToList().ForEach(resourceType =>
                    ResourceManager.Instance.RemoveResource(resourceType, buildingSO.recipe[resourceType]));
                BuildingManager.Instance.CreateBuilding(_buildingPreviewType, transform.position);
            }
            else
            {
                Debug.Log("ÀÚ¿øÀÌ ¾øÀÝ¾Æ ±×Áö ²¤²¤¾Æ");
            }
        }
    }

    private void HandleRightClickEvent(bool isDown)
    {
        SetBuilding();
    }

    private void Update()
    {
        _onUIMouse = EventSystem.current.IsPointerOverGameObject();

        if (BuildingManager.Instance.IsBuildMove == false) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(_input.MousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;
    }
}
