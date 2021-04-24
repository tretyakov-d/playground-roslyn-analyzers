using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace PlaygroundAnalyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ExceptionNameAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(Descriptors.PG0001ExceptionNameFormat);
        
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze |
                                                   GeneratedCodeAnalysisFlags.ReportDiagnostics);
            
            context.RegisterSymbolAction(action: AnalyzeNamedType, symbolKinds: SymbolKind.NamedType);
        }
        
        void AnalyzeNamedType(SymbolAnalysisContext ctx)
        {
            var symbol = (INamedTypeSymbol) ctx.Symbol;
            if (symbol.TypeKind != TypeKind.Class) return;

            if (symbol.Name.EndsWith("Exception")) return;

            if (!IsException(
                symbol, 
                ctx.Compilation.GetTypeByMetadataName(typeof(Exception).FullName))) return;
            
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    descriptor: Descriptors.PG0001ExceptionNameFormat, 
                    location: symbol.Locations.First(),
                    messageArgs: symbol.Name));
        }

        bool IsException(INamedTypeSymbol classSymbol, INamedTypeSymbol exceptionTypeSymbol)
        {
            if (classSymbol.Equals(exceptionTypeSymbol, SymbolEqualityComparer.Default)) return true;

            INamedTypeSymbol baseClass = classSymbol.BaseType;
            return baseClass != null && IsException(baseClass, exceptionTypeSymbol);
        }
    }
}