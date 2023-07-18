using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetOptionsBase : EpubInputOptionsBase
{
    [Option('o', "output", Default = OutputFormat.Json, HelpText = "Output format of data (default JSON)")]
    public OutputFormat OutputFormat { get; set; }
}