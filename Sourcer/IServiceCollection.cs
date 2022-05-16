namespace Sourcer;

public interface IServiceCollection
{
    void AddTransient<TService>()
        where TService : notnull;
    void AddScoped<TService>()
        where TService : notnull;
    void AddSingleton<TService>()
        where TService : notnull;
    void Add<TService>(ServiceLifetime serviceLifetime)
        where TService : notnull;
    void AddTransient<TService, TImplementation>()
        where TService : notnull
        where TImplementation : TService;
    void AddScoped<TService, TImplementation>()
        where TService : notnull
        where TImplementation : TService;
    void AddSingleton<TService, TImplementation>()
        where TService : notnull
        where TImplementation : TService;
    void Add<TService, TImplementation>(ServiceLifetime serviceLifetime)
        where TService : notnull
        where TImplementation : TService;
    // add methods with types as params
}
