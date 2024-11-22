using Crogen.CrogenPooling;
using UnityEngine;

public enum EAllyPigState
{
    Idle, Move, Attack, Dead
}

public class AllyPig : Ally
{
    public string OriginPoolType { get; set; }
    //GameObject IPoolingObject.gameObject { get; set; }

    //Compo
    private HealthCompo _healthCompo;

    private bool _isSelected = false;

    protected override void Awake()
    {
        base.Awake();
        _healthCompo = GetCompo<HealthCompo>();

		_healthCompo.OnDieEvent += OnDie;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _healthCompo.OnDieEvent -= OnDie;
    }

    public void OnPop()
    {
        InitComponent();
    }

    public void OnPush()
    {
        DisposeComponent();
    }

    protected override void Update()
    {
        base.Update();
	}

    private void OnDie()
    {
        //this.Push();
        Debug.Log("Á×À½");
    }
}
