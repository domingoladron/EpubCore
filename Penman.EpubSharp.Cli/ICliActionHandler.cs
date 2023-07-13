namespace Penman.EpubSharp.Cli;

public interface ICliActionHandler
{
    void HandleCliAction(object options);
}