using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

[Verb("update-authors", HelpText = "Update the author(s) in the epub.")]
public class UpdateAuthorsOptions : EpubManipulatorOptionsBase
{
    [Option('a', "author", Required = true, HelpText = "Author to add to your EPub", Min=1)]
    public IEnumerable<string> Authors { get; set; }

    [Option('c', "clear-previous", HelpText = "Clear previous authors", Default = false)]
    public bool ClearPrevious { get; set; }

}