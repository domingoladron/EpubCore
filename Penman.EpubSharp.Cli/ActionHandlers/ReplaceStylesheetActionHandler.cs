namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class ReplaceStylesheetActionHandler : ICliActionHandler
    {
        public void HandleCliAction(object options)
        {
            if (options is ReplaceStylesheetOptions replaceCoverOptions)
            {
                //do shit with them.
            }
        }
    }
}