﻿using System.IO.Abstractions;

namespace EpubCore.Cli.ActionHandlers
{
    public class UpdateTitlesActionHandler : EpubActionHandlerBase, ICliActionHandler
    {
        public UpdateTitlesActionHandler(IFileSystem fileSystem, IConsoleWriter consoleWriter) : base(fileSystem, consoleWriter)
        {
        }

        public void HandleCliAction(object options)
        {
            if (options is not UpdateTitlesOptions updateTitlesOptions) return;
            if (!RetrieveAndValidateEpubSuccessful(updateTitlesOptions)) return;

            EpubWriter!.SetTitles(updateTitlesOptions.Titles);

            SaveChanges(updateTitlesOptions);
        }
    }
}