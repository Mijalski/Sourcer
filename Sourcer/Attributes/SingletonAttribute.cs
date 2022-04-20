namespace Sourcer.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class SingletonAttribute : Attribute
{
    public Type RequestedType { get; }

    public SingletonAttribute(Type requestedType)
    {
        RequestedType = requestedType;
    }
}
