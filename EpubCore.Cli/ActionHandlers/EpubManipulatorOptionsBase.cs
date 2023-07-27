using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

public class EpubManipulatorOptionsBase : EpubInputOptionsBase
{
    [Option('o', "output-epub", Required = false, HelpText = "Path to write final epub.  If empty, overwrites input epub")]
    public string? OutputEpub { get; set; }
}