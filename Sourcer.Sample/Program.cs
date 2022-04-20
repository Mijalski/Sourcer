using Sourcer.Providers;
using Sourcer.Sample;

var services = new ServiceCollection();
services.RegisterSingleton(new DateTimeProvider());
var container = services.Build();

Console.WriteLine($"Current time: {container.GetService<DateTimeProvider>().GetUtcNow()}");
Console.WriteLine($"Current time: {container.GetService<DateTimeProvider>().GetUtcNow()}");
