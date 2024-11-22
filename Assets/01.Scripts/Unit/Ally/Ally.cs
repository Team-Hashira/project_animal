public class Ally : Unit, ISelectable
{
    public void Select(bool onSelect)
    {
        GetCompo<VisualCompo>().OutlineActive(onSelect);
    }

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new StateMachine(this);
    }
}
