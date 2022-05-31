using Microsoft.CodeAnalysis.CSharp;
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
#if DEBUG
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif
        var containerNamedTypeSymbol = ModelExtensions.GetSpeculativeTypeInfo(
                context.Compilation.GetSemanticModel(context.Compilation.SyntaxTrees.First()),
                0,
                SyntaxFactory.ParseTypeName($"{RegisteredTypesCollector.SourcerNamespace}.{RegisteredTypesCollector.SourcerContainerBuilderName}"),
                SpeculativeBindingOption.BindAsTypeOrNamespace)
            .Type as INamedTypeSymbol;

        if (containerNamedTypeSymbol is null)
        {
            return;
        }

        var registrationMethods = new List<IMethodSymbol>(containerNamedTypeSymbol.GetMembers().OfType<IMethodSymbol>().Select(m => m.OriginalDefinition));
        if (!registrationMethods.Any())
        {
            return;
        }

        if (context.SyntaxContextReceiver is not RegisteredTypesCollector receiver)
        {
            return;
        }

        foreach (var registeredType in receiver.RegisteredTypes)
        {
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
