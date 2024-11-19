using System;
using UnityEngine;

public enum CompareMode
{
	Greater,
	Equals,
	NotEqual,
	Less
}

public enum AutoUseType
{
	ValueCondition,
	ValueCheck,
	Event
}

public abstract class Skill : MonoBehaviour
{
	public bool canUse = true;

	[Header("Weapon이 활성화되었는가?")]
	public bool skillEnabled;

	[Header("쿨다운")]
	[SerializeField] protected float _cooldown;
	protected float _curCooldown;

	[Header("레벨")]
	[Range(1, 10)]
	public uint level=1;

	[Header("조건부로 발동하는 무기인가?")]
	[Tooltip("체크하면 쿨다운뿐만 아니라 조건부까지 같이 고려하여 무기를 사용합니다.")]
	public bool isConditionalSkill;

	[Header("조건부")]
	[SerializeField] protected AutoUseType _autoUseType;
	[SerializeField] protected CompareMode _compareMode;
	[SerializeField] protected float _targetValue;

	[HideInInspector] public IPlayer player;
	public event CooldownInfoEvent OnCooldownEvent;
	public event Action<float> OnWeaponUseEvent;
	[Header("적이 뭐임?")]
	public LayerMask whatIsEnemy;

	protected virtual void Update()
	{
		if (_cooldown > 0)
		{
			_curCooldown -= Time.deltaTime;
			if (_curCooldown <= 0)
			{
				_curCooldown = 0;
			}
			OnCooldownEvent?.Invoke(_curCooldown, _cooldown);
		}
	}

	public virtual void SkillInit() { }

	public void AutoUseWeaponByValueConditional(float newValue)
	{
		if (isConditionalSkill)
		{
			if (_autoUseType != AutoUseType.ValueCondition) return;
			switch (_compareMode)
			{
				case CompareMode.Greater:
					if (newValue > this._targetValue)
						UseSkill();
					return;
				case CompareMode.Equals:
					if (Mathf.Approximately(newValue, this._targetValue))
						UseSkill();
					return;
				case CompareMode.NotEqual:
					if (Mathf.Approximately(newValue, this._targetValue) == false)
						UseSkill();
					return;
				case CompareMode.Less:
					if (newValue < this._targetValue)
						UseSkill();
					return;
				default:
					return;
			}
		}
		return;
	}

	private float valueCheckPoint = 0;

	public void AutoUseSkillByValueCheck(float newValue)
	{
		if (isConditionalSkill)
		{
			if (_autoUseType != AutoUseType.ValueCheck) return;
			if (newValue - valueCheckPoint > this._targetValue)
			{
				valueCheckPoint = newValue;
				UseSkill();
			}
		}
		return;
	}

	public void AutoUseSkillByEvent()
	{
		if (isConditionalSkill)
		{
			if (_autoUseType != AutoUseType.Event) return;
			UseSkill();
		}
		return;
	}

	public virtual bool UseSkill()
	{
		if (_curCooldown > 0 || skillEnabled == false) return false;

		_curCooldown = _cooldown;
		OnWeaponUseEvent?.Invoke(level);
		return true;
	}
}