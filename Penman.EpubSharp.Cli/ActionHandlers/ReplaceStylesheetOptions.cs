using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class ReplaceStylesheetOptions : EpubManipulatorOptionsBase
{
    [Option('c', "css-path", Required = true, HelpText = "Path to new stylesheet file (.css)")]
    public string InputStylesheet { get; set; } = string.Empty;

    [Option('e', "existing-css", Required = true, HelpText = "Name of existing EPub stylesheet (.css)")]
    public string ReplaceStylesheetName { get; set; } = string.Empty;
}