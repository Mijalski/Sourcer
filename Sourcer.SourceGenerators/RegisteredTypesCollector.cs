using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace Sourcer.SourceGenerators;

public class RegisteredTypesCollector : ISyntaxContextReceiver
{
    public IList<GenericNameSyntax> RegisteredTypes = new List<GenericNameSyntax>();

    private const string BuildMethodName = "Build";
    private const string SourcerServicesCollectionClassName = "SourcerServicesCollection";
    private const string SourcerNamespace = "Sourcer";

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is ObjectCreationExpressionSyntax { Type: IdentifierNameSyntax identifierName } objectCreationExpression
            && identifierName.Identifier.ValueText.Contains(SourcerServicesCollectionClassName)
            && context.SemanticModel.GetOperation(objectCreationExpression) is IObjectCreationOperation objectCreationOperation
            && objectCreationOperation.Type?.ContainingNamespace.Name == SourcerNamespace)
        {
            Console.WriteLine("found");
        }
    }
}
