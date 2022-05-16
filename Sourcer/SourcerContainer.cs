namespace Sourcer;

public class SourcerContainer
{
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
