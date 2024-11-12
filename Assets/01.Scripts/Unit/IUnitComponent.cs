public interface IUnitComponent
{
    public void Initialize(Unit unit);
}

public interface IUnitAfterInitComponent : IUnitComponent
{
    public void AfterInit();
}

public interface IUnitDisposeComponent
{
    public void Dispose();
}
