using System.IO.Abstractions;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class UpdatePublishersActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        public UpdatePublishersActionHandler(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        public async void HandleCliAction(object options)
        {
            if (options is not UpdatePublishersOptions updateAuthorsOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(updateAuthorsOptions)) return;

            if (updateAuthorsOptions.ClearPrevious)
            {
                EpubWriter.ClearPublishers();
            }

            foreach (var curPublisher in updateAuthorsOptions.Publishers)
            {
                EpubWriter.AddPublisher(curPublisher);
            }
            
            SaveChanges(updateAuthorsOptions);
        }
    }
}