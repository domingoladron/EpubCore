namespace EpubCore.Cli;

public interface ICommandHandler
{
    Task<int> ExecuteAsync(string[] args);
}