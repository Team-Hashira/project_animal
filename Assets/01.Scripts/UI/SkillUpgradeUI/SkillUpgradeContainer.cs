using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeContainer : MonoBehaviour
{
	[SerializeField] private float _fadeDuration = 0.2f;
	[SerializeField] private SkillUIDataSO _skillUIDataSO;
	[SerializeField] private SkillUpgradeSlot[] slots;

	private SkillManager _skillManager;

	List<SkillType> skillTypes = new List<SkillType>();
	private CanvasGroup _canvasGroup;
	private RectTransform _rectTransform;
	private int _openCount = 0;
	private bool isOpenedSelectPanel = false;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_rectTransform = transform as RectTransform;
		_canvasGroup.alpha = 0;
		_canvasGroup.interactable = false;
		slots = GetComponentsInChildren<SkillUpgradeSlot>();
	}

	private void Start()
	{
		_skillManager = SkillManager.Instance;
		for (int i = 0; i < slots.Length; ++i)
		{
			slots[i].OnSelectedEndEvent += HandleSelectedEnd;
		}

		LevelManager.Instance.OnLevelUpEvent += HandleLevelUp;

		foreach (SkillType type in Enum.GetValues(typeof(SkillType)))
		{
			if(type == 0) continue;
			skillTypes.Add(type);
		}
	}

	private void Update()
	{
		if(_openCount > 0 && isOpenedSelectPanel == false)
		{
			isOpenedSelectPanel=true;
			Open();
		}
	}

	public void HandleLevelUp(int _)
	{
		Debug.Log("���ȴµ�?");
		++_openCount;
	}

	public void Open()
	{
		//����
		SkillTypeShuffle(skillTypes);

		//ī�� ����
		//ī�� ����
		List<SkillType> curskillTypes = new List<SkillType>();
		foreach (var skillType in skillTypes)
		{
			Skill skill = _skillManager.GetSkill(skillType);

			//����� �� �ִ� �����ΰ�
			if (skill.canUse)
			{
				//��� ������ �ڸ��� ����
				if (SkillManager.Instance.GetCurSkillCount() == 10)
				{
					//���ο� ���⸦ �����ٸ� �߰����� �ʰڴ�.
					if (skill.skillEnabled == false)
						continue;
				}

				//���� ���Ⱑ �ƴ϶��
				curskillTypes.Add(skillType);
			}
			//����� ��� ���ƴٸ�
			if (curskillTypes.Count >= 3)
			{
				break;
			}
		}

		//ī�� ����
		SetUpSkillSlots(curskillTypes);

		SetSkillUpgradeUI(true);
		//�÷��̾��� ���� ��� ���߱�
	}

	private void SetUpSkillSlots(List<SkillType> curSkillTypes)
	{
		for (int i = 0; i < curSkillTypes.Count; ++i)
		{
			slots[i].SetSkillInfo(
				curSkillTypes[i],
				_skillUIDataSO[curSkillTypes[i]]);
		}

		//���߱�
		Time.timeScale = 0;
	}

	public void Close()
	{
		SetSkillUpgradeUI(false).AppendCallback(() =>
		{
			--_openCount;
			isOpenedSelectPanel = false;
			//�÷��̾��� ���� �ٽ� ����
		});
	}

	private void HandleSelectedEnd()
	{
		//�ð� �ٽ� �帣��
		Time.timeScale = 1;
		Close();
	}

	private Sequence SetSkillUpgradeUI(bool isShow)
	{
		Sequence seq = DOTween.Sequence();
		seq.SetUpdate(true);
		if (isShow)
		{
			seq.AppendCallback(() => _rectTransform.anchoredPosition = new Vector2(0, -334f))
				.AppendCallback(() => _canvasGroup.interactable = false)
				.Append(_rectTransform.DOAnchorPosY(0, _fadeDuration))
				.Join(_canvasGroup.DOFade(1, _fadeDuration))
				.AppendCallback(() => _canvasGroup.interactable = true);

		}
		else
		{
			seq.AppendCallback(() => _rectTransform.anchoredPosition = Vector2.zero)
				.AppendCallback(() => _canvasGroup.interactable = false)
				.Append(_rectTransform.DOAnchorPosY(-334f, _fadeDuration))
				.Join(_canvasGroup.DOFade(0, _fadeDuration));
		}

		return seq;
	}
	private List<SkillType> SkillTypeShuffle(List<SkillType> list)
	{
		System.Random rng = new System.Random();

		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);  // 0���� n ������ ������ �ε����� ����
			SkillType value = list[k];
			list[k] = list[n];
			list[n] = value;
		}

		return list;
	}
}
