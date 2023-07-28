namespace EpubCore.Cli;

public interface ICommandHandler
{
    int Execute(string[] args);
}