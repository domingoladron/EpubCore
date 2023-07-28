using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

public class EpubInputOptionsBase
{
    [Option('i', "in", Required = true, HelpText = "Path to epub to parse")]
    public string? InputEpub { get; set; }

    [Option('v', "verbose", Required = false, Default = false, HelpText = "If true, writes heaps of logging")]
    public bool Verbose { get; set; }
}