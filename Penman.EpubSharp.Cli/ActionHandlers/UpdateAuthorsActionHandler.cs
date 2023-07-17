using System.IO.Abstractions;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class UpdateAuthorsActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        public UpdateAuthorsActionHandler(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        public async void HandleCliAction(object options)
        {
            if (options is not UpdateAuthorsOptions updateAuthorsOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(updateAuthorsOptions)) return;

            if (updateAuthorsOptions.ClearPrevious)
            {
                EpubWriter.ClearAuthors();
            }

            foreach (var curAuthor in updateAuthorsOptions.Authors)
            {
                EpubWriter.AddAuthor(curAuthor);
            }
            
            SaveChanges(updateAuthorsOptions);
        }
    }
}