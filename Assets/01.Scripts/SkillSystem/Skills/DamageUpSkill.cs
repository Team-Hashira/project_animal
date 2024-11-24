using UnityEngine;

public class DamageUpSkill : Skill
{
    private StatCompo _stat;
    private int _defaultDamage;
    public override void SkillInit()
    {
        _stat = player.GetCompo<StatCompo>();
        _defaultDamage = _stat.GetElement(EStatType.Damage).Value;
    }

    public override bool UseSkill()
    {
        if (_stat == null) return false;

        if(base.UseSkill())
        {
            _stat.GetElement(EStatType.Damage).AddModify(10, false);
        }

        return true;
    }

    protected override void Update()
    {
        base.Update();
    }
}
