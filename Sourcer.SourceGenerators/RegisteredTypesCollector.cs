using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sourcer.SourceGenerators;

public class RegisteredTypesCollector : ISyntaxContextReceiver
{
    public IList<Registration> Registrations = new List<Registration>();

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is ClassDeclarationSyntax { BaseList: { } } classDeclaration
            && classDeclaration.BaseList.Types.Any(x => x.Type.ToString() == Consts.SourcerContainerBaseName))
        {
            var methodDeclaration = classDeclaration.Members.OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(x => x.Identifier.ValueText == Consts.CreateMethodName
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

            Registrations.Add(new Registration(methodDeclaration, semanticModel));
        }
    }
}
