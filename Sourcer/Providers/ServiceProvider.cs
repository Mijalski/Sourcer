namespace Sourcer.Providers;

public interface IServiceProvider<T>
{
    T GetService();
}
