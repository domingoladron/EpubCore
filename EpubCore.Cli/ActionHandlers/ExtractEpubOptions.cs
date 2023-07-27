using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

[Verb("extract", HelpText = "Extract the contents of this EPub file")]
public class ExtractEpubOptions : EpubInputOptionsBase
{
    [Option('d', "destination", Required = true, HelpText = "path to destination directory of where to extract the EPub's files")]
    public string DestinationDirectory { get; set; }

}