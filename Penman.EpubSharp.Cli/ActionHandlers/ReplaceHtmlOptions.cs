using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("replace-html-content", HelpText = "Replace the contents of an existing html with new html content.")]
public class ReplaceHtmlOptions : EpubManipulatorOptionsBase
{
    [Option('h', "html-path", Required = true, HelpText = "Path to new html file (path/to/new.html)")]
    public string InputHtml { get; set; } = string.Empty;

    [Option('e', "existing-html", Required = true, HelpText = "Name of existing EPub html (name-of-existing.html)")]
    public string ReplaceHtmlName { get; set; } = string.Empty;
}