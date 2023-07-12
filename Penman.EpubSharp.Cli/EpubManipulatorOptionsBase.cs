using CommandLine;

namespace Penman.EpubSharp.Cli;

internal class EpubManipulatorOptionsBase
{
    [Option('i', "input", Required = true, HelpText = "Path to epub to parse")]
    public string InputEpub { get; set; }

    [Option('o', "output", Required = false, HelpText = "Path to write final epub.  If empty, overwrites input epub")]
    public string OutputEpub { get; set; }

    [Option('v', "verbose", Required = false, Default=false, HelpText = "If true, writes heaps of logging")]
    public bool Verbose { get; set; }
}