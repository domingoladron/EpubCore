using System.IO.Abstractions;
using JasperFx.Core;
using Penman.EpubSharp.Cli.Managers;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class RemoveResourceActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        private readonly IEpubResourceManager _resourceManager;

        public RemoveResourceActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IEpubResourceManager resourceManager) : base(fileSystem, consoleWriter)
        {
            _resourceManager = resourceManager;
        }

        public void HandleCliAction(object options)
        {
            if (options is not RemoveResourceOptions removeResourceOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(removeResourceOptions)) return;
            
            if (!_resourceManager.RemoveResource(EpubToProcess, removeResourceOptions.RemoveItemName, removeResourceOptions.EpubResourceType))
            {
                ConsoleWriter.WriteError($"Resource file of type {removeResourceOptions.EpubResourceType} not found: {removeResourceOptions.RemoveItemName}");
                return;
            }

            // Handle the cover page removal as well if that's the
            // image being removed
            if (removeResourceOptions.EpubResourceType.Equals(EpubResourceType.Image))
            {
                if (EpubToProcess.CoverImageHref.EqualsIgnoreCase(removeResourceOptions.RemoveItemName))
                {
                    EpubWriter.RemoveCover();
                }
            }

            SaveChanges(removeResourceOptions);
        }
    }
}