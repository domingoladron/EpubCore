using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("update-authors", HelpText = "Update the author(s) in the epub.")]
public class UpdateAuthorsOptions : EpubManipulatorOptionsBase
{
    [Option('a', "author", Required = true, HelpText = "Author to swap out of our EPub", Min=1)]
    public IEnumerable<string> Authors { get; set; }

    [Option('c', "clear-previous", HelpText = "Clear previous authors", Default = false)]
    public bool ClearPrevious { get; set; }

}