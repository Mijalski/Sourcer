using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace Sourcer.SourceGenerators;

[Generator]
public class DependencyInjectionSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
//#if DEBUG
//        if (!Debugger.IsAttached)
//        {
//            Debugger.Launch();
//        }
//#endif 
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
        var containerNamedTypeSymbol = ModelExtensions.GetSpeculativeTypeInfo(
                context.Compilation.GetSemanticModel(context.Compilation.SyntaxTrees.First()),
                0,
                SyntaxFactory.ParseTypeName($"{Consts.SourcerNamespace}.{Consts.SourcerContainerBuilderName}"),
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

        var typesToRegister = new List<(string, string, ServiceLifetime, Registration)>();
        var usings = new List<string>();

        foreach (var registration in receiver.Registrations)
        {
            var methodDeclarationBody = registration.MethodDeclaration.Body;

            if (methodDeclarationBody is null)
            {
                continue;
            }

            var namespaceSyntaxNode = registration.MethodDeclaration?.Parent?.Parent;
            usings.AddRange((namespaceSyntaxNode?.Parent as CompilationUnitSyntax)?
                .Usings
                .Select(x => x.Name as QualifiedNameSyntax)
                .Select(x => x.ToString()));

            switch (namespaceSyntaxNode) // Add base namespace to usings
            {
                case FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax:
                    usings.Add((fileScopedNamespaceDeclarationSyntax.Name as QualifiedNameSyntax)?.ToString());
                    break;
                case BaseNamespaceDeclarationSyntax baseNamespaceDeclarationSyntax:
                    usings.Add((baseNamespaceDeclarationSyntax.Name as QualifiedNameSyntax)?.ToString());
                    break;
            }


            foreach (var statement in methodDeclarationBody.Statements)
            {
                var body = statement.ToString();
                Regex.Replace(body, @"\s+", "");

                if (!body.Contains("Build()"))
                {
                    continue;
                }

                var bodyDeclarationsSplit = body.Split('<', '>');

                for (var i = 0; i < bodyDeclarationsSplit.Length; i++)
                {
                    ServiceLifetime? serviceLifetime = bodyDeclarationsSplit[i] switch
                    {
                        { } a when a.EndsWith(Consts.AddTransientMethodName) => ServiceLifetime.Transient,
                        { } a when a.EndsWith(Consts.AddScopedMethodName) => ServiceLifetime.Scoped,
                        { } a when a.EndsWith(Consts.AddSingletonMethodName) => ServiceLifetime.Singleton,
                        _ => null
                    };

                    if (!serviceLifetime.HasValue)
                    {
                        continue;
                    }

                    var implementationAndAbstractionPair = bodyDeclarationsSplit[i + 1].Split(',');
                    typesToRegister.Add((implementationAndAbstractionPair.First(),
                        implementationAndAbstractionPair.Last(),
                        serviceLifetime.Value,
                        registration));
                    i += 1;
                }
            }
        }

        if (!typesToRegister.Any())
        {
            return;
        }

        StringBuilder sourceBuilder = new StringBuilder();

        foreach (var @using in usings)
        {
            sourceBuilder.AppendLine($@"using {@using};");
        }

        sourceBuilder.Append($@"
namespace Sourcer
{{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(""Sourcer"", ""{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}"")]
    public partial class SourcerContainer
    {{
        public virtual void Create()
        {{

        }}
");

        foreach (var (implementationName, abstractionName, serviceLifetime, registration) in typesToRegister)
        {
            sourceBuilder.Append($@"
        public {implementationName} Get{abstractionName}()
        {{
            ");
            if (serviceLifetime is ServiceLifetime.Transient or ServiceLifetime.Scoped)
            {
                sourceBuilder.Append($@"
            return new {implementationName}();
        }}");
            }
            else
            {
                sourceBuilder.Append($@"
            return _{implementationName};
        }}

        private readonly {implementationName} _{implementationName} = new();");
            }

        }

        sourceBuilder.Append($@"
    }}
}}");

        context.AddSource("SourcerContainer.generated.cs", sourceBuilder.ToString());
    }
}
