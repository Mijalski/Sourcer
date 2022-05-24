using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sourcer.SourceGenerators;

public class RegisteredTypesCollector : ISyntaxContextReceiver
{
    public IList<Registration> RegisteredTypes = new List<Registration>();

    private const string CreateMethodName = "Create";
    private const string SourcerContainerBaseName = "SourcerContainer";
    private const string SourcerNamespace = "Sourcer";

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is ClassDeclarationSyntax { BaseList: { } } classDeclaration
            && classDeclaration.BaseList.Types.Any(x => x.Type.ToString() == SourcerContainerBaseName)
            && classDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
        {
            var methodDeclaration = classDeclaration.Members.OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(x => x.Identifier.ValueText == CreateMethodName
                                     && x.Modifiers.Any(m => m.IsKind(SyntaxKind.OverrideKeyword)));

            if (methodDeclaration is null)
            {
                return;
            }

            var semanticModel = context.SemanticModel.GetOperation(methodDeclaration);

            if (semanticModel is null)
            {
                return;
            }

            RegisteredTypes.Add(new Registration(methodDeclaration, semanticModel));
        }
    }
}
