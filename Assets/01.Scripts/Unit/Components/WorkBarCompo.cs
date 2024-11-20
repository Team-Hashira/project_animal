using System;
using UnityEngine;

public class WorkBarCompo : MonoBehaviour, IInitComponent
{
    [SerializeField] private GameObject _workBarObj;
    [SerializeField] private Transform _workTrm;

    private bool _isEnable;
    private Func<float> _amountFunc;

    public void Initialize(Entity entity)
    {
        HideWorkBar();
    }

    private void Update()
    {
        if (_isEnable == false) return;
        float amount = Mathf.Clamp(_amountFunc(), 0, 1);
        _workTrm.localScale = new Vector3(amount, 1, 1);
    }

    public void ShowWorkBar(Func<float> amount)
    {
        _amountFunc = amount;
        _workBarObj.SetActive(true);
        _isEnable = true;

        _workTrm.localScale = new Vector3(_amountFunc(), 1, 1);
    }

    public void HideWorkBar()
    {
        _workBarObj.SetActive(false);
        _isEnable = false;
    }
}
