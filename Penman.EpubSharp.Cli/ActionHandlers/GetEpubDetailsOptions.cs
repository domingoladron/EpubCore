using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("get-details", HelpText = "Get details about this EPub file")]
public class GetEpubDetailsOptions : GetOptionsBase
{
    [Option('f', "filter", HelpText = "Pipe-delimited filters of data you want", Separator = '|')]
    public IEnumerable<GetEpubFilterKey> Filter { get; set; }

}