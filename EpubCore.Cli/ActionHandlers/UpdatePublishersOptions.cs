﻿using CommandLine;

namespace EpubCore.Cli.ActionHandlers;

[Verb("update-publisher", HelpText = "Update the publisher(s) in the epub.")]
public class UpdatePublishersOptions : EpubManipulatorOptionsBase
{
    [Option('p', "publisher", Required = true, HelpText = "Publisher to add to your EPub", Min=1)]
    public IEnumerable<string> Publishers { get; set; } = new List<string>();

    [Option('c', "clear-previous", HelpText = "Clear previous publisher", Default = false)]
    public bool ClearPrevious { get; set; }

}