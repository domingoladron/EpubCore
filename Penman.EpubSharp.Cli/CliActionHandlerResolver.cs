using Microsoft.Extensions.DependencyInjection;
using Penman.EpubSharp.Cli.ActionHandlers;

namespace Penman.EpubSharp.Cli;

public class CliActionHandlerResolver : ICliActionHandlerResolver
{
    private readonly IServiceProvider _serviceProvider;

    public CliActionHandlerResolver(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICliActionHandler? Resolve(object obj)
    {
        switch (obj)
        {
            case ReplaceCoverOptions:
                return _serviceProvider.GetService<ReplaceCoverActionHandler>();
            case ReplaceStylesheetOptions:
                return _serviceProvider.GetService<ReplaceStylesheetActionHandler>();
            case UpdateTitlesOptions:
                return _serviceProvider.GetService<UpdateTitlesActionHandler>();
            default:
                throw new NotImplementedException();
        }
    }
}