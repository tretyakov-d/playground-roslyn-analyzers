dotnet build -p:Configuration=Release ./src/PlaygroundAnalyzers/PlaygroundAnalyzers.csproj
nuget pack ./src/PlaygroundAnalyzers/PlaygroundAnalyzers.nuspec -p Configuration=Release -outputDirectory packages