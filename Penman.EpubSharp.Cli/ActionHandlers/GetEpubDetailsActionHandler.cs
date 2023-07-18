using Penman.EpubSharp.Cli.Models;
using System.IO.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace Penman.EpubSharp.Cli.ActionHandlers;

public class GetEpubDetailsActionHandler : EpubActionHandlerBase, ICliActionHandler
{
    public GetEpubDetailsActionHandler(IFileSystem fileSystem) : base(fileSystem)
    {
    }

    public void HandleCliAction(object options)
    {
        if (options is not GetEpubDetailsOptions getEpubDetailsOptions) return;
        if (!RetrieveAndValidateEpubSuccessful(getEpubDetailsOptions)) return;

        var getEpubDetails = new GetEpubDetails(EpubToProcess, getEpubDetailsOptions.Filter.ToList());

        switch (getEpubDetailsOptions.OutputFormat)
        {
            case OutputFormat.Json:
                OutputJson(getEpubDetails);
                break;
            case OutputFormat.Yaml:
                OutputYaml(getEpubDetails);
                break;
        }
       
    }

    private void OutputYaml(GetEpubDetails getEpubDetails)
    {
        var yamlResult = new SerializerBuilder()
            .WithIndentedSequences()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .Build()
            .Serialize(getEpubDetails);

        Console.WriteLine(yamlResult);
    }

    private void OutputJson(GetEpubDetails getEpubDetails)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };
        var jsonVersion = JsonSerializer.Serialize(getEpubDetails, jsonOptions);

        Console.WriteLine(jsonVersion);
    }
}