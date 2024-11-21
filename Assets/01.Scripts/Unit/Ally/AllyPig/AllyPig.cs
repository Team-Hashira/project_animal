using Crogen.CrogenPooling;
using UnityEngine;

public enum EAllyPigState
{
    Idle
}

public class AllyPig : Unit, Ally
{
    public string OriginPoolType { get; set; }
    //GameObject IPoolingObject.gameObject { get; set; }

    //Compo
    private HealthCompo _healthCompo;

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

    public void OnSelect()
    {

    }
}
