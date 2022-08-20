using Sourcer.Sample.Services;

namespace Sourcer.Sample;

public class SampleContainer : SourcerContainer
{
    public override void Create()
    {
        new SourcerContainerBuilder()
            .AddTransient<DateTimeProvider>()
            .AddSingleton<HelloWorldService>()
            .Build();
    }
}
