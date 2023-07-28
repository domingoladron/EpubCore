using System.IO.Abstractions;
using System.Reflection;
using System.Text;
using CommandLine;
using DotMarkdown;
using EpubCore.Cli.Models;

namespace EpubCore.Cli.ActionHandlers;

public class GenerateDocumentationActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IFileSystem _fileSystem;
    private readonly IConsoleWriter _consoleWriter;
    public GenerateDocumentationActionHandler(
        IFileSystem fileSystem, 
        IConsoleWriter consoleWriter) : base(fileSystem, consoleWriter)
    {
        _fileSystem = fileSystem;
        _consoleWriter = consoleWriter;
    }

    public void HandleCliAction(object options)
    {
        if (options is not GenerateDocumentationOptions genDocOptions) return;

        var helpDocEntries = new List<HelpDocEntry>();

        var types = CommandHandler.LoadVerbs();

        foreach (var curType in types)
        {
            var verbAttribute = curType.GetCustomAttribute<VerbAttribute>();
            if (verbAttribute == null) continue;

            var helpDocEntry = new HelpDocEntry
            {
                VerbName = verbAttribute.Name,
                HelpText = verbAttribute.HelpText
            };

            var properties = curType.GetProperties();
            foreach (var curProperty in properties)
            {
                var optionVal = curProperty.GetCustomAttribute<OptionAttribute>();

                if (optionVal == null) continue;
                var helpParam = new HelpParamEntry
                {
                    HelpText = optionVal.HelpText,
                    ShortName = optionVal.ShortName,
                    LongName = optionVal.LongName,
                    Required = optionVal.Required
                };

                helpDocEntry.Parameters.Add(helpParam);
            }

            helpDocEntries.Add(helpDocEntry);
        }

        WriteMarkdownDocumentation(genDocOptions.DocumentationOutputFilePath, helpDocEntries);
    }

    private void WriteMarkdownDocumentation(string docFileName, List<HelpDocEntry> helpDocEntries)
    {
        var sb = new StringBuilder();

        using var writer = MarkdownWriter.Create(sb);
        writer.WriteHeading1("EpubCore.Cli Verbs");
        foreach (var helpDocEntry in helpDocEntries.OrderBy(g => g.VerbName))
        {
            writer.WriteHeading2(helpDocEntry.VerbName);
            writer.WriteLinkOrText(helpDocEntry.HelpText);
            if (helpDocEntry.Parameters.Any())
            {
                writer.WriteLinkOrText("");
                writer.WriteFencedCodeBlock($"epub {helpDocEntry.VerbName} <parameters>");
                writer.WriteHeading3("Parameters");
            }
            foreach (var curParameter in helpDocEntry.Parameters.OrderByDescending(g => g.Required).ThenBy(g => g.LongName))
            {
                var requiredString = curParameter.Required ? "[required]" : "";
                writer.WriteHeading4($" --{curParameter.LongName} ( -{curParameter.ShortName} ) {requiredString}");

                writer.WriteLinkOrText($"{curParameter.HelpText}");
            }
        }

        _fileSystem.File.WriteAllText(docFileName, sb.ToString());
        _consoleWriter.WriteSuccess($"epub cli documentation written to {docFileName}");
    }
}