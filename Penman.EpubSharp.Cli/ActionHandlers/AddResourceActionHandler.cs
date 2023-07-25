using System.IO.Abstractions;
using Penman.EpubSharp.Cli.Managers;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class AddResourceActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        private readonly IEpubResourceManager _resourceManager;

        public AddResourceActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IEpubResourceManager resourceManager) : base(fileSystem, consoleWriter)
        {
            _resourceManager = resourceManager;
        }

        public void HandleCliAction(object options)
        {
            if (options is not AddResourceOptions addResourceOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(addResourceOptions)) return;
            
            //if (!_resourceManager.RemoveResource(EpubToProcess, EpubWriter, addResourceOptions.RemoveItemName, addResourceOptions.EpubResourceType))
            //{
            //    ConsoleWriter.WriteError($"Resource file of type {addResourceOptions.EpubResourceType} not found: {addResourceOptions.RemoveItemName}");
            //    return;
            //}

            //SaveChanges(addResourceOptions);
        }
    }
}