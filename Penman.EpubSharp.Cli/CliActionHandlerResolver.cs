using Penman.EpubSharp.Cli.ActionHandlers;

namespace Penman.EpubSharp.Cli;

public class CliActionHandlerResolver : ICliActionHandlerResolver
{
    private readonly RunReplaceCoverActionHandler _runReplaceCoverActionHandler;
    private readonly RunReplaceStylesheetActionHandler _runReplaceStylesheetActionHandler;

    public CliActionHandlerResolver(RunReplaceCoverActionHandler runReplaceCoverActionHandler, RunReplaceStylesheetActionHandler runReplaceStylesheetActionHandler)
    {
        _runReplaceCoverActionHandler = runReplaceCoverActionHandler;
        _runReplaceStylesheetActionHandler = runReplaceStylesheetActionHandler;
    }


    public ICliActionHandler Resolve(object obj)
    {
        switch (obj)
        {
            case ReplaceCoverOptions c:
                return _runReplaceCoverActionHandler;
            case ReplaceStylesheetOptions o:
                return _runReplaceStylesheetActionHandler;
            default:
                throw new NotImplementedException();
        }
    }
}