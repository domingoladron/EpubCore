using System.IO.Abstractions;
using System.Reflection;
using System.Text;
using CommandLine;
using DotMarkdown;
using EpubCore.Cli.ActionHandlers;
using EpubCore.Cli.Models;

namespace EpubCore.Cli;

public class CommandHandler : ICommandHandler
{
    private readonly ICliErrorHandler _cliErrorHandler;
    private readonly ICliActionHandlerResolver _cliActionHandlerResolver;
    private readonly IFileSystem _fileSystem;
    private readonly IConsoleWriter _consoleWriter;

    public CommandHandler(
        ICliErrorHandler cliErrorHandler, 
        ICliActionHandlerResolver cliActionHandlerResolver, 
        IFileSystem fileSystem, 
        IConsoleWriter consoleWriter)
    {
        _cliErrorHandler = cliErrorHandler;
        _cliActionHandlerResolver = cliActionHandlerResolver;
        _fileSystem = fileSystem;
        _consoleWriter = consoleWriter;
    }

    public async Task<int> ExecuteAsync(string[] args)
    {
        var types = LoadVerbs();
        var parser = new Parser(with =>
        {
            with.HelpWriter = Console.Out;
            //ignore case for enum values
            with.CaseInsensitiveEnumValues = true;
        });

        var initialParseResult = parser.ParseArguments(args, types);
        if (initialParseResult.Value is GenerateDocumentationOptions genDocOptions)
        {
            _fileSystem.File.Delete(genDocOptions.DocumentationOutputFilePath);

            var helpDocEntries = new List<HelpDocEntry>();

            foreach (var curType in types)
            {
                var verbAttribute = curType.GetCustomAttribute<VerbAttribute>();
                if(verbAttribute == null) continue;
                
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

            return 0;
        }
        else
        {

            var result = parser.ParseArguments(args, types)
                .WithParsed(Run)
                .WithNotParsed(_cliErrorHandler.HandleError);
            if (result.Tag == ParserResultType.Parsed)
                return await Task.FromResult(0);
            return -1;
        }
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

    private void Run(object obj)
    {
        var handler = _cliActionHandlerResolver.Resolve(obj);
        handler!.HandleCliAction(obj);
    }

    private static Type[] LoadVerbs()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
    }
}