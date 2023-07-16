using Penman.EpubSharp.Cli.ActionHandlers;

namespace Penman.EpubSharp.Cli;

public class CliActionHandlerResolver : ICliActionHandlerResolver
{
    private readonly ReplaceCoverActionHandler _replaceCoverActionHandler;
    private readonly ReplaceStylesheetActionHandler _replaceStylesheetActionHandler;

    public CliActionHandlerResolver(
        ReplaceCoverActionHandler replaceCoverActionHandler, 
        ReplaceStylesheetActionHandler replaceStylesheetActionHandler)
    {
        _replaceCoverActionHandler = replaceCoverActionHandler;
        _replaceStylesheetActionHandler = replaceStylesheetActionHandler;
    }


    public ICliActionHandler Resolve(object obj)
    {
        switch (obj)
        {
            case ReplaceCoverOptions c:
                return _replaceCoverActionHandler;
            case ReplaceStylesheetOptions o:
                return _replaceStylesheetActionHandler;
            default:
                throw new NotImplementedException();
        }
    }
}