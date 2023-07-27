using System.IO.Abstractions;
using EpubCore.Cli.Factories;

namespace EpubCore.Cli.ActionHandlers;

public class GetEpubDetailsActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IMessageSerialiser _messageSerialiser;
    private readonly IGetEpubDetailsFactory _getEpubDetailsFactory;

    public GetEpubDetailsActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IMessageSerialiser messageSerialiser, IGetEpubDetailsFactory getEpubDetailsFactory) : base(fileSystem, consoleWriter)
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
                ConsoleWriter.WriteSuccess(_messageSerialiser.Serialise(getEpubDetails, OutputFormat.Json));
                break;
            case OutputFormat.Yaml:
                ConsoleWriter.WriteSuccess(_messageSerialiser.Serialise(getEpubDetails, OutputFormat.Yaml));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
}