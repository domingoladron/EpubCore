using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

[Verb("gen-cli-docs", HelpText = "Generate documentation for the CLI")]
public class GenerateDocumentationOptions
{

    [Option('o', "out", Required = true, HelpText = "File path to write documentation for the CLI")]
    public string DocumentationOutputFilePath { get; set; } = string.Empty;
}