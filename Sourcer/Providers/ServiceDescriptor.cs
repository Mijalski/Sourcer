using Sourcer.Attributes;

namespace Sourcer.Providers;

public class ServiceDescriptor
{
    public object Implementation { get; }
    public ServiceLifetime Lifetime { get; }

    public ServiceDescriptor(object implementation, ServiceLifetime lifetime)
    {
        Implementation = implementation;
        Lifetime = lifetime;
    }
}
