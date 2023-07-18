namespace Penman.EpubSharp.Cli;

public interface IMessageSerialiser
{
    string Serialise(object objectToSerialise, OutputFormat outputFormat);
}