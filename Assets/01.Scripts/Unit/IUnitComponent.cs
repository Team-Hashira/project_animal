public interface IUnitInitializeComponent
{
    public void Initialize(Unit unit);
}

public interface IUnitAfterInitComponent
{
    public void AfterInit();
}

public interface IUnitDisposeComponent
{
    public void Dispose();
}
