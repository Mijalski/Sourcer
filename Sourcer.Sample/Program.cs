using Sourcer;
using Sourcer.Sample;

var services = new SourcerServicesCollection();

var container = services.Build();

container.CreateScope();

Console.WriteLine($"Current time: {container.GetService<DateTimeProvider>().GetUtcNow()}");
Console.WriteLine($"Current time: {container.GetService<DateTimeProvider>().GetUtcNow()}");
