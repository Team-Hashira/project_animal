public interface ISelectable
{
    public void Select(bool onSelect);
}

public interface IAlly : ISelectable
{
    public void OnSelect();
}
