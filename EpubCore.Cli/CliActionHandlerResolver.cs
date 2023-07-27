using EpubCore.Cli.ActionHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace EpubCore.Cli;

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
            case ReplaceHtmlOptions:
                return _serviceProvider.GetService<ReplaceHtmlActionHandler>();
            case UpdateTitlesOptions:
                return _serviceProvider.GetService<UpdateTitlesActionHandler>();
            case UpdateAuthorsOptions:
                return _serviceProvider.GetService<UpdateAuthorsActionHandler>();
            case UpdatePublishersOptions:
                return _serviceProvider.GetService<UpdatePublishersActionHandler>();
            case GetEpubDetailsOptions:
                return _serviceProvider.GetService<GetEpubDetailsActionHandler>();
            case ExtractEpubOptions:
                return _serviceProvider.GetService<ExtractEPubActionHandler>();
            case GetHtmlOptions:
                return _serviceProvider.GetService<GetHtmlActionHandler>();
            case RemoveResourceOptions:
                return _serviceProvider.GetService<RemoveResourceActionHandler>();
            default:
                throw new NotImplementedException();
        }
    }
}