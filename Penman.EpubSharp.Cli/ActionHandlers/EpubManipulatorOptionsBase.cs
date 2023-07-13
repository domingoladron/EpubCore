using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class EpubManipulatorOptionsBase
{
    [Option('i', "input-epub", Required = true, HelpText = "Path to epub to parse")]
    public string InputEpub { get; set; }

    [Option('o', "output-epub", Required = false, HelpText = "Path to write final epub.  If empty, overwrites input epub")]
    public string OutputEpub { get; set; }

    [Option('v', "verbose", Required = false, Default = false, HelpText = "If true, writes heaps of logging")]
    public bool Verbose { get; set; }
}