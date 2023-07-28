dotnet tool uninstall --global EpubCore.Cli

dotnet pack EpubCore.Cli/EpubCore.Cli.csproj

dotnet tool install --global --add-source ./nupkg EpubCore.Cli