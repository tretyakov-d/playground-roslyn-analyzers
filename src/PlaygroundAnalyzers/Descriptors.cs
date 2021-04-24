using Microsoft.CodeAnalysis;

namespace PlaygroundAnalyzers
{
    static class Descriptors
    {
        internal static readonly DiagnosticDescriptor PG0001ExceptionNameFormat = new(
            id: "PG0001",
            title: "Exception class name should end with Exception",
            messageFormat: "{0} class name should end with Exception",
            category: "Naming",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
    }
}