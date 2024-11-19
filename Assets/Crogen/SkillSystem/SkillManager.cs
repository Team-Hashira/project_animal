using System.Collections.Generic;
using System;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager _instance;
    public static SkillManager Instance 
    { 
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<SkillManager>();
            }
            return _instance;
        }
    }
    public event Action<SkillType> OnAppendSkillEvent;
    private Dictionary<SkillType, Skill> _skills;

    [SerializeField] private List<Skill> _curSkills; //해금된 자동발동 무기 리스트
    private IPlayer _player;

    private void Awake()
	{
        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (var monoBehaviour in monoBehaviours)
        {
            _player = monoBehaviour.GetComponent<IPlayer>();
            if (_player != null) break;
        }

        _skills = new Dictionary<SkillType, Skill>();
        _curSkills = new List<Skill>();
    }

    public int GetCurSkillCount() => _curSkills.Count;

	private void Start()
    {
        foreach (SkillType skillEnum in Enum.GetValues(typeof(SkillType)))
        {
            if (skillEnum == SkillType.None) continue;
            Type t = Type.GetType($"{skillEnum.ToString()}Skill");
            Skill skillCompo = GetComponentInChildren(t) as Skill;
            _skills.Add(skillEnum, skillCompo);

            //초반에 활성화된 무기 추가(거의 사실 상 디버그용)
            if (skillCompo.skillEnabled)
                AppendSkill(skillEnum);
        }
    }

    public Skill GetSkill(SkillType skill)
	{
        if (skill == SkillType.None) return null;

        if(_skills.TryGetValue(skill, out Skill skillCompo))
		{
            return skillCompo as Skill;
        }

        return null;
    }

    [ContextMenu("DebugAppendSkill")]
    private void DebugAppendSkill()
    {
        AppendSkill((SkillType)1);   
    }

    public void AppendSkill(SkillType skill)
    {
        //이미 있다면 레벨업
        if(_curSkills.Contains(_skills[skill]))
		{
            ++_skills[skill].level;
            return;
		}

        //전에 없다면 초기화
        _skills[skill].player = _player;
		_skills[skill].SkillInit();
        _skills[skill].skillEnabled = true;
        _curSkills.Add(_skills[skill]);
        OnAppendSkillEvent?.Invoke(skill);
    }

    private void Update()
    {
        foreach (var skill in _curSkills)
        {
            if (skill.isConditionalSkill) continue;
            skill.UseSkill();
        }
    }
}