using DG.Tweening;
using System.Resources;
using UnityEngine;

public class RecipeArea : MonoBehaviour
{
    [SerializeField] private ResourceDataSO _resourceDataSO;
    [SerializeField] private Transform _recipe;
    [SerializeField] private ResourceCountUI _resourceCountUI;

    private Sequence _openSeq;
    private RectTransform _rectTrm;
    private int _resourceCnt;

    public void Init(BuildingSO buildingSO)
    {
        _rectTrm = transform as RectTransform;
        _rectTrm.sizeDelta = new Vector2(140, -10);

        _resourceCnt = buildingSO.recipe.Keys.Count;
        foreach (EResourceType resourceType in buildingSO.recipe.Keys)
        {
            ResourceCountUI resourceCountUI = Instantiate(_resourceCountUI, _recipe);
            resourceCountUI.Init(_resourceDataSO[resourceType].sprite, buildingSO.recipe[resourceType]);
        }
    }

    public void Show()
    {
        if (_openSeq != null && _openSeq.IsActive()) _openSeq.Kill();
        _openSeq = DOTween.Sequence();
        _openSeq.Append(_rectTrm.DOSizeDelta(new Vector2(140, _resourceCnt * 40), 0.2f).SetEase(Ease.OutExpo));
    }

    public void Close()
    {
        if (_openSeq != null && _openSeq.IsActive()) _openSeq.Kill();
        _openSeq = DOTween.Sequence();
        _openSeq.Append(_rectTrm.DOSizeDelta(new Vector2(140, -10), 0.2f).SetEase(Ease.InSine));
    }
}
