using Microsoft.CodeAnalysis.Text;

namespace Sourcer.SourceGenerators;

[Generator]
public class DependencyInjectionSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif 
        context.RegisterForSyntaxNotifications(() => new RegisteredTypesCollector());
    }

    public void Execute(GeneratorExecutionContext context)
    {
//#if DEBUG
//        if (!Debugger.IsAttached)
//        {
//            Debugger.Launch();
//        }
//#endif 
        if (context.SyntaxContextReceiver is not RegisteredTypesCollector receiver)
        {
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append($@"
namespace Sourcer
{{
    public class GeneratedX()
    {{
asdsad
    }}
}}
");

        //context.AddSource("Generated", SourceText.From(sb.ToString(), Encoding.UTF8));
    }
}
