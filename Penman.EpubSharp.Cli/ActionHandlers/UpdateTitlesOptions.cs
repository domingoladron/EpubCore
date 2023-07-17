using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("update-titles", HelpText = "Update the title(s) in the epub.")]
public class UpdateTitlesOptions : EpubManipulatorOptionsBase
{
    [Option('t', "titles", Required = true, HelpText = "Titles to swap out of our EPub", Min=1)]
    public IEnumerable<string> Titles { get; set; }

}