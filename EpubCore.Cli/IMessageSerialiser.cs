namespace EpubCore.Cli;

public interface IMessageSerialiser
{
    string Serialise(object objectToSerialise, OutputFormat outputFormat);
}