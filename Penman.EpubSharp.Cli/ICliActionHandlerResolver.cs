namespace Penman.EpubSharp.Cli;

public interface ICliActionHandlerResolver
{
    ICliActionHandler Resolve(object obj);
}