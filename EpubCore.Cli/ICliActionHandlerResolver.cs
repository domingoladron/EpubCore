namespace EpubCore.Cli;

public interface ICliActionHandlerResolver
{
    ICliActionHandler? Resolve(object obj);
}