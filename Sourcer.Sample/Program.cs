using Sourcer.Sample;

var container = new SampleContainer();
var dateTimeProvider = container.GetDateTimeProvider();
Console.WriteLine(dateTimeProvider.GetUtcNow());

var helloWorldService = container.GetHelloWorldService();
Console.WriteLine(helloWorldService.SayHello());

