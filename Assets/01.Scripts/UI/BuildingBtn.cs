using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RecipeArea _resourceArea;
    private Building _createTargetPrefab;
    private Button _button;

    public void Init(BuildingSO buildingSO, UnityAction action)
    {
        _button = GetComponent<Button>();
        _button.image.sprite = buildingSO.sprite;
        _button.onClick.AddListener(action);
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
