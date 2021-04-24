using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Xunit;

namespace PlaygroundAnalyzers.Tests
{
    public class ExceptionNameCodeFixFacts : CSharpCodeFixTest<ExceptionNameAnalyzer, ExceptionNameCodeFix, XUnitVerifier>
    {
        [Fact]
        public async Task WhenInconsistentName_AddsExceptionPostfix()
        {
            TestCode = "public class CustomError : System.Exception { }";

            ExpectedDiagnostics.Add(
                new DiagnosticResult(Descriptors.PG0001ExceptionNameFormat.Id, DiagnosticSeverity.Warning)
                    .WithMessage("CustomError class name should end with Exception")
                    .WithSpan(1, 14, 1, 25));
            
            FixedCode = "public class CustomErrorException : System.Exception { }";

            await RunAsync();
        }
    }
}