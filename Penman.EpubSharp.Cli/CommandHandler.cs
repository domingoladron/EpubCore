using CommandLine;
using System.Reflection;

namespace Penman.EpubSharp.Cli;

public class CommandHandler : ICommandHandler
{
    private readonly ICliErrorHandler _cliErrorHandler;
    private readonly ICliActionHandlerResolver _cliActionHandlerResolver;

    public CommandHandler(
        ICliErrorHandler cliErrorHandler, 
        ICliActionHandlerResolver cliActionHandlerResolver)
    {
        _cliErrorHandler = cliErrorHandler;
        _cliActionHandlerResolver = cliActionHandlerResolver;
    }

    public async Task<int> ExecuteAsync(string[] args)
    {
        var types = LoadVerbs();
        var parser = new Parser(with =>
        {
            //ignore case for enum values
            with.CaseInsensitiveEnumValues = true;
        });

        var result = parser.ParseArguments(args, types)
            .WithParsed(Run)
            .WithNotParsed(_cliErrorHandler.HandleError);
        if (result.Tag == ParserResultType.Parsed)
            return await Task.FromResult(0);
        return -1;
    }

    private void Run(object obj)
    {
        var handler = _cliActionHandlerResolver.Resolve(obj);
        handler.HandleCliAction(obj);
       
    }

    private static Type[] LoadVerbs()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
    }
}