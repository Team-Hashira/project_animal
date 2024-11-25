using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeSlot : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _skillNameText;
	[SerializeField] private TextMeshProUGUI _levelText;
	[SerializeField] private TextMeshProUGUI _descriptionText;

	private SkillManager _skillManager;

	private Button _button;
	private RectTransform _rectTransform;
	public event Action OnSelectedEndEvent;
	private SkillType _currentSkillType;

	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(HandleSelectSlot);

		_rectTransform = transform as RectTransform;

    }

	private void Start()
	{
		_skillManager = SkillManager.Instance;
	}

	private void HandleSelectSlot()
	{
		_skillManager.AppendSkill(_currentSkillType);
		OnSelectedEndEvent?.Invoke();
		_button.interactable = false;
	}

	public void SetSkillInfo(SkillType skillType, SkillUIData uiData)
	{
		_button.interactable = true;
		_currentSkillType = skillType;

		Skill curWeapon = _skillManager.GetSkill(skillType);

		// PowerSO를 받아서 등록 해야함    
		_icon.sprite = uiData.icon;
		_skillNameText.text = uiData.skillName;
		if (curWeapon.skillEnabled) //이미 가지고 있으면
			_levelText.text = $"level {curWeapon.level.ToString()} > level {(curWeapon.level + 1).ToString()}";
		else
			_levelText.text = "New";
		_descriptionText.text = uiData.description;
	}

	public Sequence Show()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
		seq.AppendCallback(() => _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, 1200))
			.Append(_rectTransform.DOAnchorPosY(0, 0.2f));

		return seq;
    }

	public Sequence Hide()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
		seq.Append(_rectTransform.DOAnchorPosY(1200, 0.2f));

        return seq;
    }
}
