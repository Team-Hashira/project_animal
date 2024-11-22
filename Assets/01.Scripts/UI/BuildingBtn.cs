using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RecipeArea _resourceArea;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    private Building _createTargetPrefab;
    private Button _button;

    public void Init(BuildingSO buildingSO, UnityAction action)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(action);
        _image.sprite = buildingSO.sprite;
        _name.text = buildingSO.buildingName;
        _createTargetPrefab = buildingSO.building;

        _resourceArea.Init(buildingSO);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _resourceArea.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _resourceArea.Close();
    }
}
