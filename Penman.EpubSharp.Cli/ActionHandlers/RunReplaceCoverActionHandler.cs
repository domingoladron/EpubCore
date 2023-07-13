namespace Penman.EpubSharp.Cli.ActionHandlers;

public class RunReplaceCoverActionHandler : ICliActionHandler
{
    public void HandleCliAction(object options)
    {
        if (options is ReplaceCoverOptions replaceCoverOptions)
        {
            //do shit with them.
        }
    }
}