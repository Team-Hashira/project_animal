using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCountUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    public void Init(Sprite sprite, int count)
    {
        _image.sprite = sprite;
        _text.text = count.ToString();
    }
}
