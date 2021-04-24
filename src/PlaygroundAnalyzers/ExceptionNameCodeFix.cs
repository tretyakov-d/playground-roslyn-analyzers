using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace PlaygroundAnalyzers
{
    [ExportCodeFixProvider(LanguageNames.CSharp), Shared]
    public class ExceptionNameCodeFix : CodeFixProvider
    {
        public override ImmutableArray<string> FixableDiagnosticIds { get; }
            = ImmutableArray.Create(Descriptors.PG0001ExceptionNameFormat.Id);

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
            SyntaxNode node = root?.FindNode(context.Span);

            if (node is not ClassDeclarationSyntax classDeclaration) return;

            SyntaxToken identifier = classDeclaration.Identifier;

            Document document = context.Document;
            Solution solution = document.Project.Solution;
            SemanticModel documentSemanticModel = await document.GetSemanticModelAsync(context.CancellationToken);
            ISymbol classModel = documentSemanticModel.GetDeclaredSymbol(classDeclaration, context.CancellationToken);
            string suggestedName = $"{identifier.Text}Exception";

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: $"Rename to {suggestedName}",
                    createChangedSolution: async cancellationToken => await Renamer.RenameSymbolAsync(
                        solution,
                        classModel,
                        suggestedName,
                        solution.Workspace.Options,
                        cancellationToken)),
                context.Diagnostics);

            await Task.CompletedTask;
        }
    }
}