﻿using System.IO.Abstractions;
using Penman.EpubSharp.Cli.Managers;

namespace Penman.EpubSharp.Cli.ActionHandlers
{
    public class RemoveHtmlActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        private readonly IEpubResourceManager _resourceManager;

        public RemoveHtmlActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter, IEpubResourceManager resourceManager) : base(fileSystem, consoleWriter)
        {
            _resourceManager = resourceManager;
        }

        public void HandleCliAction(object options)
        {
            if (options is not RemoveResourceOptions removeHtmlOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(removeHtmlOptions)) return;
            
            if (!_resourceManager.RemoveHtml(EpubToProcess, removeHtmlOptions.RemoveItemName))
            {
                ConsoleWriter.WriteError($"Html file not found: {removeHtmlOptions.RemoveItemName}");
                return;
            }

            SaveChanges(removeHtmlOptions);
        }
    }
}