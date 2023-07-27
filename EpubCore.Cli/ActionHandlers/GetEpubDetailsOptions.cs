using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

[Verb("get-details", HelpText = "Get details about this EPub file")]
public class GetEpubDetailsOptions : GetOptionsBase
{
    [Option('f', "filter", HelpText = "Pipe-delimited filter keys of the data you want to be returned.  Filter values include: Uniqueidentifier|Version|Authors|Publishers|Contributors|Titles|Toc|Css|Cover|Images|Fonts|Html", Separator = '|')]
    public IEnumerable<GetEpubFilterKey> Filter { get; set; }

}