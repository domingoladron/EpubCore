using System.Text.Json;
using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace EpubCore.Cli;

public class MessageSerialiser : IMessageSerialiser
{
    public string Serialise(object objectToSerialise, OutputFormat outputFormat)
    {
        switch (outputFormat)
        {
            case OutputFormat.Json:
                var jsonOptions = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                };
                return JsonSerializer.Serialize(objectToSerialise, jsonOptions);
            case OutputFormat.Yaml:
                return new SerializerBuilder()
                    .WithIndentedSequences()
                    .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
                    .Build()
                    .Serialize(objectToSerialise);
            default:
                throw new ArgumentOutOfRangeException(nameof(outputFormat), outputFormat, null);
        }
    }
}