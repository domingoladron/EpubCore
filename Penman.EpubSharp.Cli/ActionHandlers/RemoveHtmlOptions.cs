using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("remove-html", HelpText = "Remove an existing html from the EPub.")]
public class RemoveHtmlOptions : EpubManipulatorOptionsBase
{

    [Option('e', "existing-html", Required = true, HelpText = "Name of existing EPub html (name-of-existing.html)")]
    public string RemoveHtmlName { get; set; } = string.Empty;
}