using System.IO.Abstractions;
using Penman.EpubSharp.Cli.Factories;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetEpubDetailsActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IMessageSerialiser _messageSerialiser;
    private readonly IGetEpubDetailsFactory _getEpubDetailsFactory;

    public GetEpubDetailsActionHandler(IFileSystem fileSystem, IMessageSerialiser messageSerialiser, IGetEpubDetailsFactory getEpubDetailsFactory) : base(fileSystem)
    {
        _messageSerialiser = messageSerialiser;
        _getEpubDetailsFactory = getEpubDetailsFactory;
    }

    public void HandleCliAction(object options)
    {
        if (options is not GetEpubDetailsOptions getEpubDetailsOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(getEpubDetailsOptions)) return;

        var getEpubDetails = _getEpubDetailsFactory.Create(EpubToProcess, getEpubDetailsOptions.Filter.ToList());

        switch (getEpubDetailsOptions.OutputFormat)
        {
            case OutputFormat.Json:
                Console.WriteLine(_messageSerialiser.Serialise(getEpubDetails, OutputFormat.Json));
                break;
            case OutputFormat.Yaml:
                Console.WriteLine(_messageSerialiser.Serialise(getEpubDetails, OutputFormat.Yaml));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
}