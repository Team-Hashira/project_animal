using UnityEngine;
using Crogen.CrogenPooling;
 
public class ResourceObject : Entity
{
    [SerializeField] private ResourceSO _resourceSO;
    [SerializeField] private Vector2Int _resourceGetCount;
    [SerializeField] private EffectPoolType _hitEffect, _getResourceEffect;

    private HealthCompo _healthCompo;

    protected override void Awake()
    {
        base.Awake();
        _healthCompo = GetCompo<HealthCompo>();
        _healthCompo.OnHealthChangedEvent += HandleHealthChangedEvent;
        _healthCompo.OnDieEvent += HandleDieEvent;
    }

    protected override void OnDestroy()
    {
        _healthCompo.OnDieEvent -= HandleDieEvent;
    }

    private void HandleHealthChangedEvent(int prevHealth, int newHealth, bool isVisible)
    {
        if (prevHealth > newHealth)
        {
            gameObject.Pop(_hitEffect, transform.position, Quaternion.identity);
        }
    }

    private void HandleDieEvent()
    {
        _healthCompo.Resurrection();
		ParticleSystem effect = (gameObject.Pop(_getResourceEffect, transform.position, Quaternion.identity) as MonoBehaviour).GetComponent<ParticleSystem>();
        effect.textureSheetAnimation.SetSprite(0, _resourceSO.sprite);
        
        ResourceManager.Instance.AddResource(_resourceSO.resourceType, Random.Range(_resourceGetCount.x, _resourceGetCount.y));
    }
}
