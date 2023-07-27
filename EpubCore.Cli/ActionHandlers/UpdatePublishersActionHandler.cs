using System.IO.Abstractions;

namespace EpubCore.Cli.ActionHandlers
{
    public class UpdatePublishersActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        public UpdatePublishersActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter) : base(fileSystem, consoleWriter)
        {
        }

        public void HandleCliAction(object options)
        {
            if (options is not UpdatePublishersOptions updateAuthorsOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(updateAuthorsOptions)) return;

            if (updateAuthorsOptions.ClearPrevious)
            {
                EpubWriter!.ClearPublishers();
            }

            foreach (var curPublisher in updateAuthorsOptions.Publishers)
            {
                EpubWriter!.AddPublisher(curPublisher);
            }
            
            SaveChanges(updateAuthorsOptions);
        }
    }
}