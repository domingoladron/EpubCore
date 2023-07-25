using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("remove-resource", HelpText = "Remove an existing resource from the EPub.")]
public class RemoveResourceOptions : EpubManipulatorOptionsBase
{

    [Option('e', "existing-resource", Required = true, HelpText = "Name of existing EPub resource (name-of-existing.html)")]
    public string RemoveItemName { get; set; } = string.Empty;

    [Option('t', "resource-type", Required = true, HelpText = "Type of EPub resource (Html, Css, Font, Image, Other)")]
    public EpubResourceType EpubResourceType { get; set; }
}