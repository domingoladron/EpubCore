using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("update-authors", HelpText = "Update the author(s) in the epub.")]
public class UpdateAuthorsOptions : EpubManipulatorOptionsBase
{
    [Option('a', "authors", Required = true, HelpText = "Authors to swap out of our EPub", Min=1)]
    public IEnumerable<string> Authors { get; set; }

    [Option('c', "clear-previous", HelpText = "Clears previous authors")]
    public bool ClearPrevious { get; set; }

}