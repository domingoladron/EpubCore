using CommandLine;

namespace Penman.EpubSharp.Cli.ActionHandlers;

[Verb("add-resource", HelpText = "Add a new resource from the EPub.")]
public class AddResourceOptions : EpubManipulatorOptionsBase
{

    [Option('a', "add-resource", Required = true, HelpText = "Path to new new EPub resource to add")]
    public string AddItemName { get; set; } = string.Empty;

    [Option('b', "before-resource", HelpText = "Add before this existing EPub resource (name-of-existing.html)")]
    public string BeforeItemName { get; set; } = string.Empty;

    [Option('t', "resource-type", Required = true, HelpText = "Type of EPub resource (Html, Css, Font, Image, Other)")]
    public EpubResourceType EpubResourceType { get; set; }
}