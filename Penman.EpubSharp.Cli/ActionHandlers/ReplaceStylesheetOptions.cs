using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class ReplaceStylesheetOptions : EpubManipulatorOptionsBase
{
    [Option('s', "stylesheet", Required = true, HelpText = "Path to new stylesheet file (.css)")]
    public string InputStylesheet { get; set; }
}