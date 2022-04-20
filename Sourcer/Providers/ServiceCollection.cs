using Sourcer.Attributes;

namespace Sourcer.Providers;

public class ServiceCollection
{
    private readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

    public void RegisterSingleton<TService>(TService implementation)
    {
        if (implementation is null)
        {
            throw new ArgumentNullException(nameof(implementation));
        }

        _serviceDescriptors.Add(implementation.GetType(), new ServiceDescriptor(implementation, ServiceLifetime.Singleton));
    }

    public Container Build()
    {
        return new Container(_serviceDescriptors);
    }
}
