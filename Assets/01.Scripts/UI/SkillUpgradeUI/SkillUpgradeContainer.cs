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
		Debug.Log("열렸는데?");
		++_openCount;
	}

	public void Open()
	{
		//셔플
		SkillTypeShuffle(skillTypes);

		//카드 결정
		//카드 결정
		List<SkillType> curskillTypes = new List<SkillType>();
		foreach (var skillType in skillTypes)
		{
			Skill skill = _skillManager.GetSkill(skillType);

			//등록할 수 있는 무기인가
			if (skill.canUse)
			{
				//등록 가능한 자리가 없고
				if (SkillManager.Instance.GetCurSkillCount() == 10)
				{
					//새로운 무기를 만난다면 추가하지 않겠다.
					if (skill.skillEnabled == false)
						continue;
				}

				//전용 무기가 아니라면
				curskillTypes.Add(skillType);
			}
			//등록을 모두 마쳤다면
			if (curskillTypes.Count >= 3)
			{
				break;
			}
		}

		//카드 셋팅
		SetUpSkillSlots(curskillTypes);

		SetSkillUpgradeUI(true);
		//플레이어의 동작 모두 멈추기
	}

	private void SetUpSkillSlots(List<SkillType> curSkillTypes)
	{
		for (int i = 0; i < curSkillTypes.Count; ++i)
		{
			slots[i].SetSkillInfo(
				curSkillTypes[i],
				_skillUIDataSO[curSkillTypes[i]]);
		}

		//멈추기
		Time.timeScale = 0;
	}

	public void Close()
	{
		SetSkillUpgradeUI(false).AppendCallback(() =>
		{
			--_openCount;
			isOpenedSelectPanel = false;
			//플레이어의 동작 다시 실행
		});
	}

	private void HandleSelectedEnd()
	{
		//시간 다시 흐르게
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
			int k = rng.Next(n + 1);  // 0부터 n 사이의 무작위 인덱스를 선택
			SkillType value = list[k];
			list[k] = list[n];
			list[n] = value;
		}

		return list;
	}
}
