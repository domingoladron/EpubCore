using System.IO.Abstractions;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class UpdateTitlesActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        public UpdateTitlesActionHandler(IFileSystem fileSystem) : base(fileSystem)
        {
        }

        public async void HandleCliAction(object options)
        {
            if (options is not UpdateTitlesOptions updateTitlesOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(updateTitlesOptions)) return;

            EpubWriter.SetTitles(updateTitlesOptions.Titles);

            SaveChanges(updateTitlesOptions);
        }
    }
}