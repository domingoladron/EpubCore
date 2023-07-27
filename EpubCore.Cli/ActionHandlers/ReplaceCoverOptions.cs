using CommandLine;
using static System.String;

namespace EpubCore.Cli.ActionHandlers;

[Verb("replace-cover", HelpText = "Replace the cover image in the epub.")]
public class ReplaceCoverOptions : EpubManipulatorOptionsBase
{
    [Option('c', "cover-img", Required = true, HelpText = "Path to new cover image (.jpg, .png)")]
    public string InputCoverImage { get; set; } = Empty;

}