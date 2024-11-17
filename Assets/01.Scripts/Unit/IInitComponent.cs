using UnityEngine;

public interface IInitComponent
{
    public void Initialize(Entity entity);
}

public interface IAfterInitComponent : IInitComponent
{
    public void AfterInit();
}

public interface IDisposeComponent
{
    public void Dispose();
}
