namespace Sourcer;

public class SourcerServicesCollection
{
    public SourcerContainer Build()
    {
        return new SourcerContainer();
    }

    public void AddTransient<TService>() where TService : notnull => throw new NotImplementedException();

    public void AddScoped<TService>() where TService : notnull => throw new NotImplementedException();

    public void AddSingleton<TService>() where TService : notnull => throw new NotImplementedException();

    public void Add<TService>(ServiceLifetime serviceLifetime) where TService : notnull => throw new NotImplementedException();

    public void AddTransient<TService, TImplementation>() where TService : notnull where TImplementation : TService => throw new NotImplementedException();

    public void AddScoped<TService, TImplementation>() where TService : notnull where TImplementation : TService => throw new NotImplementedException();

    public void AddSingleton<TService, TImplementation>() where TService : notnull where TImplementation : TService => throw new NotImplementedException();

    public void Add<TService, TImplementation>(ServiceLifetime serviceLifetime) where TService : notnull where TImplementation : TService => throw new NotImplementedException();
}
