using System.IO.Abstractions;
using EpubCore.Cli.Managers;

namespace EpubCore.Cli.ActionHandlers
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
            
            if (!_resourceManager.RemoveResource(EpubToProcess!, EpubWriter!, removeResourceOptions.RemoveItemName, removeResourceOptions.EpubResourceType))
            {
                ConsoleWriter.WriteError($"Resource file of type {removeResourceOptions.EpubResourceType} not found: {removeResourceOptions.RemoveItemName}");
                return;
            }

            SaveChanges(removeResourceOptions);
        }
    }
}