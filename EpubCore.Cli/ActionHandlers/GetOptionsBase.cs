using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

public class GetOptionsBase : EpubInputOptionsBase
{
    [Option('f', "format", Default = OutputFormat.Json, HelpText = "Output format of data (Json or Yaml)")]
    public OutputFormat OutputFormat { get; set; }
}