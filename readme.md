# Roslyn analyzer/fixer sample project

## ./src/PlaygroundAnalyzers

ExceptionNameAnalyzer.cs - shows warning when exception class name does not end with Exception

ExceptionNameFix.cs - suggests (and does) add Exception postfix to a class name

PlaygroundAnalyzers.nuspec - analyzer nuget package spec.


## ./tests/PlaygroundAnalyzers.Tests

Tests project requires packages **Microsoft.CodeAnalysis.CSharp.Analyzer.Testing.&lowast;**  
which by some reason are not on public nuget.

roslyn analyzer nuget repository https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-tools/nuget/v3/index.json added to NuGet.Config in a solution root. 


## ./examples/ExampleAssembly

An example assembly to see it's really working.
Should work out of the box in any IDE that supports roslyn analyzers (eg. Visual Studio, JetBrains Rider and VS Code).  

It users package PlaygroundAnalyzers from local folder ./packages/

Therefore ./packages/ added as a source to a NuGet.Config in solution root.

### Trying some changes

After code modifications are done, increment version in ./src/PlaygroundAnalyzers/PlaygroundAnalyzers.nuspec
and run **./make.sh/** (or **make.bat** on windows) to make a new package.

Update package reference in ExampleAssembly to a new one.
To see the change effect, restart IDE/editor, at least if you using Rider, Visual Studio or VS Code.



