namespace Sourcer;

public class SourcerContainerBuilder
{
    public SourcerContainerBuilder AddTransient<TService>() where TService : notnull => throw new NotImplementedException();

    public SourcerContainerBuilder AddScoped<TService>() where TService : notnull => throw new NotImplementedException();

    public SourcerContainerBuilder AddSingleton<TService>() where TService : notnull => throw new NotImplementedException();
    
    public SourcerContainerBuilder AddTransient<TService, TImplementation>() where TService : notnull where TImplementation : TService => throw new NotImplementedException();

    public SourcerContainerBuilder AddScoped<TService, TImplementation>() where TService : notnull where TImplementation : TService => throw new NotImplementedException();

    public SourcerContainerBuilder AddSingleton<TService, TImplementation>() where TService : notnull where TImplementation : TService => throw new NotImplementedException();

    public SourcerContainer Build() => throw new NotImplementedException();
}
