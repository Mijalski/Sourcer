using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sourcer.SourceGenerators;

public class Registration
{
    public Registration(MethodDeclarationSyntax methodDeclaration, IOperation operation)
    {
        MethodDeclaration = methodDeclaration;
        Operation = operation;
    }

    public MethodDeclarationSyntax MethodDeclaration { get; }
    public IOperation Operation { get; }
}
