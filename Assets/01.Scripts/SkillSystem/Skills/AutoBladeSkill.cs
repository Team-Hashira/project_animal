using Unity.VisualScripting;
using UnityEngine;

public class AutoBladeSkill : Skill
{
    private StatCompo _stat;
    private Collider2D[] _colliders;
    [SerializeField]
    private Vector2 _detectSize;
    private int _detectCount = 15;
    [SerializeField]
    private ParticleSystem _attackEffect;

    public override void SkillInit()
    {
        _stat = player.GetCompo<StatCompo>();
        _colliders = new Collider2D[_detectCount];
    }

    public override bool UseSkill()
    {
        if(base.UseSkill())
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(player.Input.MousePosition) - player.transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.y) * Mathf.Rad2Deg - 90;
            Vector2 centerPos = (Vector2)player.transform.position + direction * (_detectSize.x / 2);
            ContactFilter2D contactFilter = new ContactFilter2D() { useLayerMask = true, layerMask = whatIsEnemy, useTriggers = true};
            int count = Physics2D.OverlapBox(centerPos, _detectSize, angle, contactFilter, _colliders);
            if(count > 0)
            {
                for(int i = 0; i < count; i++)
                {
                    if(_colliders[i].TryGetComponent(out IDamageable target))
                    {
                        target.ApplyDamage(10, EStatType.Damage, _stat);
                    }
                }
            }
            ParticleSystem effect = Instantiate(_attackEffect, centerPos, Quaternion.identity);
            var main = effect.main;
            main.startRotation = new ParticleSystem.MinMaxCurve(angle, angle);
            effect.transform.localScale = new Vector2(_detectSize.x * 2, _detectSize.y * 2);
            effect.Play();
        }
        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
