using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct SkillUIData
{
    public SkillType skillType;
	public Sprite icon;
    public string skillName;
    [TextArea]
    public string description;
}

[CreateAssetMenu(menuName = "SO/Skill/SkillUIData")]
public class SkillUIDataSO : ScriptableObject
{
    public SkillUIData this[SkillType key]
    {
        get
        {
            return skillDataList.First(x=>x.skillType == key);
		}
	}
    public List<SkillUIData> skillDataList;

    private void Reset()
    {
		skillDataList = new List<SkillUIData>();

		foreach (SkillType value in Enum.GetValues(typeof(SkillType)))
        {
            skillDataList.Add(new SkillUIData() { skillType = value});
        }
    }
}
