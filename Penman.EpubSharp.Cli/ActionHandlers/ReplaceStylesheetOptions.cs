using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("replace-css-content", HelpText = "Replace the contents of an existing css with new css.")]
public class ReplaceStylesheetOptions : EpubManipulatorOptionsBase
{
    [Option('c', "css-path", Required = true, HelpText = "Path to new stylesheet file (path/to/new.css)")]
    public string InputStylesheet { get; set; } = string.Empty;

    [Option('e', "existing-css", Required = true, HelpText = "Name of existing EPub stylesheet (name-of-existing.css)")]
    public string ReplaceStylesheetName { get; set; } = string.Empty;
}