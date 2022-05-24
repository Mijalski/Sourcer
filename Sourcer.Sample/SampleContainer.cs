namespace Sourcer.Sample;

public partial class SampleContainer : SourcerContainer
{
    public override void Create()
    {
        new SourcerContainerBuilder()
            .AddTransient<DateTimeProvider>()
            .Build();
    }
}
