using Sourcer.Providers;

namespace Sourcer;

public class Container
{
    private readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors;

    public Container(IDictionary<Type, ServiceDescriptor> serviceDescriptors)
    {
        _serviceDescriptors = serviceDescriptors;
    }

    public TService GetService<TService>()
    {
        if (_serviceDescriptors.TryGetValue(typeof(TService), out var descriptor))
        {
            return (TService)descriptor.Implementation;
        }

        throw new InvalidOperationException($"Service type {typeof(TService)} is unregistered");
    }
}
