namespace Sourcer;

public abstract class SourcerContainer
{
    public abstract void Create();

    public SourcerScope CreateScope()
    {
        throw new NotImplementedException();
    }

    public T GetService<T>() where T : notnull
    {
        throw new NotImplementedException();
    }

    public object GetService(Type serviceType)
    {
        throw new NotImplementedException();
    }

}
