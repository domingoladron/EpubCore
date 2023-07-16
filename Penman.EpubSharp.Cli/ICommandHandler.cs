namespace Penman.EpubSharp.Cli;

public interface ICommandHandler
{
    Task<int> ExecuteAsync(string[] args);
}