using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatElement
{
    [SerializeField] private int _baseValue;
    [SerializeField] private List<int> _addModifies = new List<int>();
    [SerializeField] private List<int> _percentModifies = new List<int>();

    public int Value { get; private set; }

    public StatElement(int baseValue)
    {
        _baseValue = baseValue;
        SetValue();
    }

    private void SetValue()
    {
        //���� ������� ����
        int numValue = _baseValue;
        for (int i = 0; i < _addModifies.Count; i++)
        {
            numValue += _addModifies[i];
        }

        //�ۼ�Ʈ ������� ����
        int percentModify = 0;
        for (int i = 0; i < _percentModifies.Count; i++)
        {
            percentModify += _percentModifies[i];
        }

        //���� ���� ���� �� �ۼ�Ʈ ���� ����
        int value = Mathf.RoundToInt(numValue * (1 + (float)percentModify / 10000));

        Value = value;
    }

    public void AddModify(int modify, bool _isPercentModify)
    {
        if (_isPercentModify)
            _percentModifies.Add(modify);
        else
            _addModifies.Add(modify);
        SetValue();
    }
    public void RemoveModify(int modify, bool _isPercentModify)
    {
        if (_isPercentModify && _percentModifies.Contains(modify))
            _percentModifies.Add(modify);
        else if (_addModifies.Contains(modify))
            _addModifies.Add(modify);
        SetValue();
    }
}
