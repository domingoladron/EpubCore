using Penman.EpubSharp.Cli.Models;
using System.IO.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetEpubDetailsActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    private readonly IMessageSerialiser _messageSerialiser;

    public GetEpubDetailsActionHandler(IFileSystem fileSystem, IMessageSerialiser messageSerialiser) : base(fileSystem)
    {
        _messageSerialiser = messageSerialiser;
    }

    public void HandleCliAction(object options)
    {
        if (options is not GetEpubDetailsOptions getEpubDetailsOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(getEpubDetailsOptions)) return;

        var getEpubDetails = new GetEpubDetails(EpubToProcess, getEpubDetailsOptions.Filter.ToList());

        switch (getEpubDetailsOptions.OutputFormat)
        {
            case OutputFormat.Json:
                Console.WriteLine(_messageSerialiser.Serialise(getEpubDetails, OutputFormat.Json));
                break;
            case OutputFormat.Yaml:
                Console.WriteLine(_messageSerialiser.Serialise(getEpubDetails, OutputFormat.Yaml));
                break;
        }
       
    }
}