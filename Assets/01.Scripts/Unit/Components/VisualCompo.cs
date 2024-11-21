using DG.Tweening;
using UnityEngine;
using UnityEngine.Android;

[RequireComponent(typeof(SpriteRenderer))]
public class VisualCompo : MonoBehaviour, IInitComponent
{
    private Entity _owner;
    private SpriteRenderer _spriteRenderer;
    private Sequence _blinkSeq;
    private Material _material;

    private readonly int _BlinkPropertyHash = Shader.PropertyToID("_Blink");
    private readonly int _OutlineWidthPropertyHash = Shader.PropertyToID("_OutlineWidth");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
    }

    public void Initialize(Entity entity)
    {
        _owner = entity;
    }

    public void Blink(float time, float inTime = 0, float outTime = 0)
    {
        if (_blinkSeq != null && _blinkSeq.IsActive()) _blinkSeq.Kill();
        _blinkSeq = DOTween.Sequence();

        _blinkSeq.Append(_material.DOFloat(1, _BlinkPropertyHash, inTime));
        _blinkSeq.AppendInterval(time);
        _blinkSeq.Append(_material.DOFloat(0, _BlinkPropertyHash, outTime));
    }

    public void OutlineActive(bool inOn)
    {
        _material.SetFloat(_OutlineWidthPropertyHash, inOn ? 2 : 0);
    }
}
